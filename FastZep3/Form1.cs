using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.IO;
using System.Collections;
using System.Xml;
using System.Net;
using Security.Cryptography;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
/*
 Copyright Ludovit Scholtz 2009-2010
 ludovit@scholtz.sk
 */
namespace FastZep3
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        public Form1()
        {
            InitializeComponent();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reloadCerts();
            loadPolicy();

            ArrayList al = new ArrayList();
            string select = "";
            Hashtable hashtable = new Hashtable();
            foreach (CapiNative.CRYPT_OID_INFO oid in CapiNative.EnumerateOidInformation(Security.Cryptography.OidGroup.HashAlgorithm)) {
                BinaryObject obj = new BinaryObject(oid.pszOID, oid.pwszName);
                
                if (oid.pwszName == "sha256") select = oid.pszOID;
                if (hashtable[oid.pszOID] == null || (bool)hashtable[oid.pszOID] == false)
                {
                    al.Add(obj);
                    hashtable[oid.pszOID] = true;
                }
            }
            comboBox1.DataSource = al;
            comboBox2.DataSource = al;
            if (al.Count > 0)
            {
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Id";
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Id";
                comboBox2.SelectedValue = select;
                comboBox1.SelectedValue = select;
            }
            
            loadOProgrameText();
            groupBox4.BringToFront();

            try
            {
                textBox1.Text = FZRegistry.get("lastSignFileInput");
            }
            catch { }
            try
            {
                textBox2.Text = FZRegistry.get("lastSignFileOutput");
            }
            catch { }
            try
            {
                textBox3.Text = FZRegistry.get("lastSignFolderInput");
            }
            catch { }
            try
            {

                textBox4.Text = FZRegistry.get("lastSignFolderOutput");
            }
            catch { }

            
        }
        private byte[] getPolicy(bool full) {
            if (dataGridViewPolitiky.SelectedRows.Count != 1) {
                throw new Exception("V menu \"Nastavenia>Podpisová politika\" si vyberte podpisovú politiku ktorou chcete podpispovať dokument a stlačte tlačítko uložiť.");
            }
            try
            {
                string id = dataGridViewPolitiky.SelectedRows[0].Cells["id"].Value.ToString() + ".der";
                byte[] file = Convert.FromBase64String(FZRegistry.get(id));
                if (full) return file;
                byte[] policy = createShortPolicy(file, true);
                return policy;
            }
            catch (Exception exc) {
                throw new Exception("Došlo ku chybe spracovania podpisovej politky. " + exc.Message);
            }
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button2.Text = "Multi podpis";
            FZRegistry.set("lastSignFolderInput", textBox3.Text);
            FZRegistry.set("lastSignFolderOutput", textBox4.Text);
            
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.richTextBox1.Text += "\n" + e.UserState.ToString();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.Focus();
        }
        private delegate string getCB1Delegate();
        private string getCB1Value(){
            if (comboBox1.InvokeRequired) {
                return comboBox1.Invoke(new getCB1Delegate(this.getCB1Value)).ToString();
            }
            return comboBox1.SelectedValue.ToString();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            bw.ReportProgress(0, "Zacinam podpisovat");

            string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
            foreach (string file in files)
            {
                try
                {
                    if (!Directory.Exists(folderBrowserDialog2.SelectedPath)) {
                        Directory.CreateDirectory(folderBrowserDialog2.SelectedPath);
                    }
                    if (signFile(file, folderBrowserDialog2.SelectedPath + @"\" + Path.GetFileName(file) + ".zep", getCB1Value(), checkBox2.Checked))
                    {
                        bw.ReportProgress(0, "Subor " + Path.GetFileName(file) + " bol podpisany");
                        if (bw.CancellationPending)
                        {
                            bw.ReportProgress(0, "Stopnute");
                            return;
                        }

                    }
                }
                catch (Exception exc)
                {

                    bw.ReportProgress(0, "Subor " + Path.GetFileName(file) + " nebol podpisany: " + exc.Message + "");
                    if (bw.CancellationPending)
                    {
                        bw.ReportProgress(0, "Stopnute");
                        return;
                    }
                }
            }
        }
        BackgroundWorker bw = new BackgroundWorker();
       
        private bool signFile(string file, string outFile, string algoritmus, bool detached) {
            try
            {
                string saveTo = Path.GetDirectoryName(file);
                string tmpDir = FastZep.FastZepFolder + "tmp";
                if (lbValue == "") {
                    return false;
                }
                if (Directory.Exists(tmpDir))
                {
                    Directory.Delete(tmpDir, true);
                }
                Directory.CreateDirectory(tmpDir);

                string date = DateTime.Now.Subtract(TimeSpan.FromHours(1)).ToString("yyyyMMddHHmmss") + "Z";

                Directory.CreateDirectory(tmpDir + "\\D" + date + "\\Policy\\");
                string certFile = tmpDir + "\\D" + date + "\\Policy\\P" + date + ".der";
                string sigFile = tmpDir + "\\D" + date + "\\S" + date + ".p7s";

                string emlFile = tmpDir + "\\D" + date + "\\M" + date + ".eml";
                Directory.CreateDirectory(tmpDir + "\\D" + date + "\\");
                TextWriter eml = new StreamWriter(emlFile);
                eml.WriteLine("MIME-Version: 1.0");
                eml.WriteLine("Content-Type: " + MimeType(file));
                eml.WriteLine("Content-Transfer-Encoding: base64");
                eml.WriteLine("Content-Disposition: filename=\"" + Path.GetFileName(file) + "\"");
                eml.WriteLine();
                FileStream fs = new FileStream(file, FileMode.Open);
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                string encodedData = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
                eml.Write(encodedData);
                eml.Close();
                fs.Close();

                byte[] buffer = File.ReadAllBytes(emlFile);
                ContentInfo contentInfo = new ContentInfo(buffer);
                X509Store store = new X509Store();
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySerialNumber, lbValue, false);
                int i = 0;
                foreach (X509Certificate2 cert in certs)
                {
                    i++;
                    SignedCms signedCms = new SignedCms(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, !detached);
                    CmsSigner cmsSigner = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, cert);
                    
                    cmsSigner.SignedAttributes.Add(new Pkcs9SigningTime());
                    cmsSigner.IncludeOption = X509IncludeOption.WholeChain;

                    ESSCertIDv2 cer2 = new ESSCertIDv2(cert);
                    X509Chain chain = new X509Chain();
                    chain.Build(cert);

                    /*
                    


                    /**/

                    //cmsSigner.UnsignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.21", CertRefs.get(chain)));
                    //cmsSigner.UnsignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.22", CertCrls.get(chain)));
                    //cmsSigner.UnsignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.23", OtherCerts.get(chain)));
                    //cmsSigner.UnsignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.24", OtherCrls.get(chain)));
                    /**/
                    //cmsSigner.SignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.47", cer2.get()));
                    //cmsSigner.SignedAttributes.Add(new AsnEncodedData("1.2.840.113549.1.9.16.2.15", getPolicy(false)));


                    /**/
                    signedCms.ComputeSignature(cmsSigner, false);
                    File.WriteAllBytes(certFile, getPolicy(true));
                    File.WriteAllBytes(sigFile, signedCms.Encode());
                }
                if (i == 0)
                {
                    throw new Exception("Failed" + Marshal.GetLastWin32Error().ToString());
                }
                SignedCms signedCms2 = new SignedCms();


                byte[] encodedMessage = File.ReadAllBytes(sigFile);
                signedCms2.Decode(encodedMessage);

                BHI.Rats.RatsCompressionManager.Zip(tmpDir, outFile);
                Directory.Delete(tmpDir, true);
                return true;
            }
            catch (CryptographicException e)
            {
                var error = e.Message.ToString();
                if (error == "Unknown error \"-1073741275\".") {
                    error = "Nepodarilo sa načítať certifikát. odpojte a pripojte čítačku.";
                }
                if (error == "An internal error occurred.\r\n") {
                    error = "Došlo ku chybe pri podpisovaní. Pravdepodobne nemáte pripojený USB kľúč alebo čítačku kariet.";
                }
                Console.WriteLine("Signing failed: " + error.ToString());
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + e.InnerException.ToString());
                }
                MessageBox.Show(error, "Chyba pri podpisovaní", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + Marshal.GetLastWin32Error().ToString(), "Chyba pri podpisovaní", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        
        }
        string verifiedFileName = "";
        private void overSubor(string file) {
            string tmpadr = FastZep.FastZepFolder + "verify";
            if(Directory.Exists(tmpadr)) Directory.Delete(tmpadr,true);
            Directory.CreateDirectory(tmpadr);
            try
            {
                BHI.Rats.RatsCompressionManager.Extract(file, tmpadr);
            }
            catch (Exception exc) {
                validity.Text = "Súbor nie je platný ZEP súbor. "+exc.Message;
                return;
            }
            dataGridView1.Rows.Clear();
            string myfileEml = "";
            string[] files = Directory.GetFiles(tmpadr, "*.eml", SearchOption.AllDirectories);
            if(files.Length > 0){
                myfileEml = files[0];
            }
            if (myfileEml != "")
            {
                bool indata = false;
                verifiedFileName = "";
                String data = "";
                foreach (string line in File.ReadAllLines(myfileEml))
                {
                    if (indata) { data += line; }
                    else
                    {
                        if (line.IndexOf("Content-Disposition:") >= 0)
                        {
                            int start = line.IndexOf("\"") + 1;
                            int last = line.IndexOf("\"", start + 1);
                            verifiedFileName = line.Substring(start, last - start);
                        }
                        if (line == "") indata = true;
                    }
                }
                if (verifiedFileName == "")
                {
                    validity.Text = "Nepodarilo sa extrahovať súbor!"; return;
                }
                File.WriteAllBytes(tmpadr + @"\" + verifiedFileName, Convert.FromBase64String(data));
            }

            SignedCms cms;
            if (myfileEml != "")
            {
                cms = new SignedCms(new ContentInfo(File.ReadAllBytes(myfileEml)), true);
            }
            else {
                cms = new SignedCms();
                if (cms.ContentInfo.Content.Length == 0) {
                    validity.Text = "Overovaný súbor neobsahuje žiadny podpísaný súbor!"; return;
                }
            }
            try
            {
                validity.Text = "Platný";
                foreach (string myfile in Directory.GetFiles(tmpadr, "*.p7s", SearchOption.AllDirectories))
                {
                    cms.Decode(File.ReadAllBytes(myfile));
                    cms.CheckHash();
                    cms.CheckSignature(true);
                    try
                    {
                        cms.CheckSignature(false);
                    }
                    catch (Exception exc) {
                        validity.Text = "Platný, ale nastali problémy s certifikátmi: " + exc.Message;
                    }
                    string signdate = "";

                    foreach (CryptographicAttributeObject atr in cms.SignerInfos[0].SignedAttributes)
                    {
                        //File.WriteAllBytes(@"C:\tmp\out\F" + atr.Oid.Value.ToString() + ".asn", atr.Values[0].RawData);
                        switch (atr.Oid.Value.ToString())
                        {
                            case "1.2.840.113549.1.9.3":
                                LipingShare.LCLib.Asn1Processor.Oid oid = new LipingShare.LCLib.Asn1Processor.Oid();
                                
                                
                                string oidstr = oid.Decode(atr.Values[0].RawData);
                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    oidstr = oid.Decode(node.getValue());
                                }
                                string value = "";
                                if (oidstr == "1.2.840.113549.1.7.1") value = "Dáta (1.2.840.113549.1.7.1)";
                                if (oidstr == "1.2.840.113549.1.7.2") value = "Podpísané dáta (1.2.840.113549.1.7.2)";
                                if (oidstr == "1.2.840.113549.1.7.3") value = "Obalené dáta (1.2.840.113549.1.7.3)";
                                if (oidstr == "1.2.840.113549.1.7.4") value = "Podpísané a obalené dáta (1.2.840.113549.1.7.4)";
                                if (oidstr == "1.2.840.113549.1.7.5") value = "Zahašované dáta (1.2.840.113549.1.7.5)";
                                if (oidstr == "1.2.840.113549.1.7.6") value = "Zašifrované dáta (1.2.840.113549.1.7.6)";

                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.3", true, "Obsah", value });
                                break;
                            case "1.2.840.113549.1.9.4":
                                value = "";
                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    value = BitConverter.ToString(node.getValue()).Replace("-", "");
                                }
                                File.WriteAllBytes(tmpadr + @"\S" + atr.Oid.Value.ToString() + ".cer", atr.Values[0].RawData);
                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.4", true, "Hash", value });
                                break;
                            case "1.2.840.113549.1.9.5":
                                value = "";
                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                                    value = enc.GetString(node.getValue());
                                    signdate = value;
                                }
                                File.WriteAllBytes(tmpadr + @"\S" + atr.Oid.Value.ToString() + ".cer", atr.Values[0].RawData);
                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.5", true, "Čas podpisu", value });
                                break;
                            case "1.2.840.113549.1.9.16.2.12":
                                value = "";
                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    foreach (ASNNode node2 in node.getChilds())
                                    {
                                        foreach (ASNNode node3 in node2.getChilds())
                                        {
                                            foreach (ASNNode node4 in node3.getChilds())
                                            {
                                                foreach (ASNNode node5 in node4.getChilds())
                                                {
                                                    if (node5.Type == 2)
                                                    {
                                                        // identifikator
                                                        string mySerial = BitConverter.ToString(node5.getValue()).Replace("-", "");
                                                        foreach (X509Certificate2 cert in cms.Certificates)
                                                        {
                                                            string serial = cert.GetSerialNumberString();
                                                            if (serial == mySerial)
                                                            {
                                                                File.WriteAllBytes(tmpadr + @"\S1.2.840.113549.1.9.16.2.12.cer", cert.GetRawCertData());
                                                                value = "Dvojklik pre zobrazenie";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                File.WriteAllBytes(tmpadr + @"\S" + atr.Oid.Value.ToString() + ".cer", atr.Values[0].RawData);
                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.16.2.12", true, "Podpisový certifikát", value });
                                break;
                            case "1.2.840.113549.1.9.16.2.15":
                                value = "";
                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    ASNNode node2 = node.getChilds().First();
                                    File.WriteAllBytes(tmpadr + @"\S1.2.840.113549.1.9.16.2.15.cer", node.getValue());
                                    oid = new LipingShare.LCLib.Asn1Processor.Oid();
                                    value = oid.Decode(node2.getValue());

                                }
                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.16.2.15", true, "Podpisová politika", value });
                                break;
                            case "1.2.840.113549.1.9.16.2.47":
                                
                                File.WriteAllBytes(tmpadr + @"\S1.2.840.113549.1.9.16.2.47.cer", atr.Values[0].RawData);
                                dataGridView1.Rows.Add(new object[] { "1.2.840.113549.1.9.16.2.47", true, "Podpisový certifikát 2", BitConverter.ToString(atr.Values[0].RawData).Replace("-", "") });
                                break;
                            default:
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), true, "", BitConverter.ToString(atr.Values[0].RawData).Replace("-", "") });
                                break;
                        }
                    }
                    foreach (CryptographicAttributeObject atr in cms.SignerInfos[0].UnsignedAttributes)
                    {
                        File.WriteAllBytes(tmpadr + @"\U" + atr.Oid.Value.ToString() + ".cer", atr.Values[0].RawData);
                        switch (atr.Oid.Value.ToString())
                        {
                            case "1.2.840.113549.1.9.16.2.14":
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "Podpis obsahuje časovú pečiatku", "" });
                                break;
                            case "1.2.840.113549.1.9.16.2.21":
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "Odkazy na certifikáty", "" });
                                break;
                            case "1.2.840.113549.1.9.16.2.22":
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "Odkazy na CRL (zneplatnené certifikáty)", "" });
                                break;
                            case "1.2.840.113549.1.9.16.2.23":
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "Zoznam certifikátov", "" });
                                break;
                            case "1.2.840.113549.1.9.16.2.24":

                                foreach (ASNNode node in ASNNode.parse(atr.Values[0].RawData))
                                {
                                    foreach (ASNNode node2 in node.getChilds())
                                    {
                                        foreach (ASNNode node3 in node2.getChilds())
                                        {
                                            foreach (ASNNode node4 in node3.getChilds())
                                            {
                                                foreach (ASNNode node5 in node4.getChilds())
                                                {
                                                    string from = "";
                                                    string until = "";
                                                    foreach (ASNNode node6 in node5.getChilds())
                                                    {
                                                        if (node6.Type == 0x17)
                                                        {
                                                            if (from == "")
                                                            {
                                                                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                                                                from = enc.GetString(node6.getValue());
                                                            }
                                                            else
                                                            {
                                                                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                                                                until = enc.GetString(node6.getValue());
                                                                int i = from.CompareTo(signdate);
                                                                if (signdate != "")
                                                                    if (from.CompareTo(signdate) == -1 && signdate.CompareTo(until) == -1)
                                                                    {
                                                                        // ok
                                                                    }
                                                                    else
                                                                    {
                                                                        // qsign bug
                                                                        validity.Text = "Uznávaný (Podpis obsahuje chybu neplatného CRL spôsobeným chybou aplikácie QSign.)";

                                                                    }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "Zoznam CRL", "" });
                                break;
                            default:
                                dataGridView1.Rows.Add(new object[] { atr.Oid.Value.ToString(), false, "", BitConverter.ToString(atr.Values[0].RawData).Replace("-", "") });
                                break;
                        }

                    }
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                
            }
            catch (Exception exc) {
                validity.Text = "Neplatný: "+exc.Message;
            }
            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        class OtherCrls
        {
            public static byte[] get(X509Chain chain)
            {
                ASNNode root = new ASNNode(AsnTag.SEQUENCE);
                ASNNode node;
                root.AppendChild(node = new ASNNode(AsnTag.CONTEXT_SPECIFIC1));
                node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                int i = 0;
                foreach (X509ChainElement chainEl in chain.ChainElements)
                {
                    i++;
                    ArrayList adresyCRL = new ArrayList();
                    foreach (X509Extension ext in chainEl.Certificate.Extensions)
                    {
                        if (ext.Oid.Value == "2.5.29.31")
                        {
                            adresyCRL = Crls.generateCrlAddresses(ext.RawData);
                        }
                    }
                    foreach (string url in adresyCRL)
                    {
                        byte[] crl = Crls.getCrl(url);
                        if (crl.Length > 0)
                            node.AppendChild(new ASNNode(crl, AsnTag.RAW_DATA));
                    }


                }


                return root.get();
            }

        }
        class OtherCerts {
            public static byte[] get(X509Chain chain)
            {
                ASNNode root = new ASNNode(AsnTag.SEQUENCE);
                int i = 0;
                foreach (X509ChainElement chainEl in chain.ChainElements)
                {
                    i++;
                    if (i == 1) continue;
                    root.AppendChild(new ASNNode(chainEl.Certificate.RawData,AsnTag.RAW_DATA));

                }
                return root.get();
            }
        }
        class CertRefs
        {
            public static byte[] get(X509Chain chain)
            {
                ASNNode root = new ASNNode(AsnTag.SEQUENCE);
                ASNNode node;
                ASNNode node2;
                SHA256 shaM = new SHA256Managed();
                int i = 0;
                foreach (X509ChainElement chainEl in chain.ChainElements)
                {
                    i++;
                    if (i == 1) continue;
                    root.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node2 = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(new ASNNode(shaM.ComputeHash(chainEl.Certificate.RawData), AsnTag.OCTET_STRING));
                    node2.AppendChild(new ASNNode(new System.Security.Cryptography.Oid("2.16.840.1.101.3.4.2.1")));
                    node2.AppendChild(new ASNNode(AsnTag.NULL));
                }
                return root.get();
            }

        }
        class Crls {
            public static ArrayList generateCrlAddresses(byte[] certCrl) {
                UTF8Encoding enc = new UTF8Encoding();
                ArrayList adresyCRL = new ArrayList();
                ArrayList list = ASNNode.parse(certCrl);
                ASNNode[] nodes = ((ASNNode)list[0]).getChilds();
                string adr = "";
                bool haveWeb = false;
                foreach (ASNNode node1 in nodes)
                {
                    foreach (ASNNode node3 in node1.getChilds())
                    {
                        if (haveWeb) continue;
                        foreach (ASNNode node4 in node3.getChilds())
                        {
                            if (haveWeb) continue;
                            foreach (ASNNode node5 in node4.getChilds())
                            {
                                if (haveWeb) continue;
                                if (node5.getChilds().Length == 0)
                                {
                                    adr = enc.GetString(node5.getValue());
                                    adresyCRL.Add(adr);
                                    if (adr.Substring(0, 7) == "http://")
                                    {
                                        haveWeb = true;
                                    }
                                }
                                foreach (ASNNode node6 in node5.getChilds())
                                {
                                    if (haveWeb) continue;
                                    if (node6.getChilds().Length == 0)
                                    {
                                        adr = enc.GetString(node6.getValue());
                                        adresyCRL.Add(adr);
                                        if (adr.Substring(0, 7) == "http://") {
                                            haveWeb = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return adresyCRL;
            }
            public static Hashtable cache = new Hashtable();
            public static byte[] getCrl(string addr) {
                byte[] crl = new byte[0];
                if (cache.ContainsKey(addr)) return (byte[]) cache[addr];
                if (addr.Substring(0, 7) == "http://")
                {
                    WebClient client = new WebClient();
                    crl = client.DownloadData(addr);
                    cache[addr] = crl;
                }
                return crl;
            }

        }
        class CertCrls
        {
            
            public static byte[] get(X509Chain chain)
            {

                
                ASNNode root = new ASNNode(AsnTag.SEQUENCE);
                ASNNode node;
                ASNNode node2;
                int i = 0;
                foreach (X509ChainElement chainEl in chain.ChainElements)
                {
                    i++;
                    byte[] hash = null;
                    SHA256 shaM = new SHA256Managed();
                    UTF8Encoding enc = new UTF8Encoding();
                    ArrayList adresyCRL = new ArrayList() ;
                    foreach (X509Extension ext in chainEl.Certificate.Extensions) {
                        if (ext.Oid.Value == "2.5.29.31") {
                            adresyCRL = Crls.generateCrlAddresses(ext.RawData);
                        }
                    }
                    foreach (string url in adresyCRL) {
                        byte[] crl = Crls.getCrl(url);
                        if (crl.Length > 0)
                        {
                            hash = shaM.ComputeHash(crl);
                        }
                    }
                    if (hash == null) continue;
                    

                    root.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node = new ASNNode(AsnTag.CONTEXT_SPECIFIC1));
                    node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(node2 = new ASNNode(AsnTag.SEQUENCE));
                    node2.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                    node.AppendChild(new ASNNode(new System.Security.Cryptography.Oid("2.16.840.1.101.3.4.2.1")));
                    node.AppendChild(new ASNNode(AsnTag.NULL));
                    node2.AppendChild(node = new ASNNode(hash));
                    node.Type = AsnTag.OCTET_STRING;
                }
                return root.get();
            }

        }

        class ESSCertIDv2
        {
            byte[] ret;
            public ESSCertIDv2(X509Certificate2 cert)
            {
                //                ASNTree root = new ASNTree(AsnTag.SEQUENCE);
                ASNNode root = new ASNNode(AsnTag.SEQUENCE);
                ASNNode node;
                ASNNode node2;
                ASNNode node3;
                root.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                node2 = new ASNNode(cert.GetCertHash());
                node2.Type = AsnTag.OCTET_STRING;
                node.AppendChild(node2);
                node3 = new ASNNode(cert.GetSerialNumber());
                node3.Type = AsnTag.INTEGER;
                node3.ReverseData();

                node.AppendChild(node = new ASNNode(AsnTag.SEQUENCE));
                node.AppendChild(node2 = new ASNNode(AsnTag.SEQUENCE));
                node.AppendChild(node3);
                node2.AppendChild(node = new ASNNode(AsnTag.CONTEXT_SPECIFIC));
                node.AppendChild(new ASNNode(cert.IssuerName.RawData, AsnTag.RAW_DATA));


                ret = root.get();
                return;

            }
            public byte[] get()
            {
                return ret;
            }
        }
        private static byte[] mergeArr(byte[] ar1,byte[] ar2,bool reverse){
            byte[] ret = new byte[ar1.Length + ar2.Length];
            int i = 0;
            foreach (byte b in ar1)
            {
                ret[i] = b;
                i++;
            }
            if (reverse) {
                for (int j = ar2.Length - 1; j >= 0; j--)
                {
                    ret[i] = ar2[j];
                    i++;
                }
            }
            else
            {
                foreach (byte b in ar2)
                {
                    ret[i] = b;
                    i++;
                }

            }
            return ret;
        }
        private static byte[] mergeArr(byte[] ar1, byte[] ar2) {
            return mergeArr(ar1,ar2,false);
        }
        Hashtable zoznam = new Hashtable();

        private bool showOnlyZEPCertificates = true;
        private void reloadCerts()
        {
            X509Store store = new X509Store();
            store.Open(OpenFlags.ReadOnly);
            ArrayList al = new ArrayList();
            foreach (X509Certificate2 cert in store.Certificates)
            {
                X509Chain chain = new X509Chain();
                chain.Build(cert);
                
                string rootHash = "";
                try
                {
                    foreach(X509ChainElement cert2 in chain.ChainElements){
                        rootHash = cert2.Certificate.GetCertHashString();
                    }
                }
                catch { }
                string[] nbuCertHashes = { 
                  "A6D7D70982CB73BE7FA69470029E7EF9360EEA68", 
                  "4EA3F1135F43A4D521973DAA1FBEB3CDF2DCF75A", 
                  "21F73B27BBBF2811BBEAB4F1799E7DD892F3FE85",
                  //"4FE7E75D543A5063654F8457A77D050FC5816873",
                };

                if (showOnlyZEPCertificates)
                {
                    bool certIsNBU = false;
                    foreach(string certSHA1 in nbuCertHashes){
                        if (rootHash == certSHA1) certIsNBU = true;
                    }
                    if(!certIsNBU){
                        continue;
                    }
                }
                if (DateTime.Now > cert.NotAfter) continue;
                if (DateTime.Now < cert.NotBefore) continue;


                zoznam[cert.SerialNumber] = cert;
                string name = cert.Issuer;
                File.WriteAllBytes(FastZep.FastZepFolder + cert.SerialNumber + ".cer", cert.GetRawCertData());
                if (cert.Subject != "") name += ": " + cert.Subject;
                al.Add(new BinaryObject(cert.SerialNumber, name));
            }
            store.Close();
            listBox1.DataSource = al;
            listBox2.DataSource = al;
            if (al.Count > 0)
            {
                listBox1.DisplayMember = "Value";
                listBox1.ValueMember = "Id";
                listBox2.DisplayMember = "Value";
                listBox2.ValueMember = "Id";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                button2.Text = "Stop";
                folderBrowserDialog1.SelectedPath = textBox3.Text;
                if (folderBrowserDialog1.SelectedPath == "")
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                folderBrowserDialog2.SelectedPath = textBox4.Text;
                if (folderBrowserDialog2.SelectedPath == "")
                if (folderBrowserDialog2.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                bw.RunWorkerAsync();
            }
            else
            {
                button2.Text = "Multi podpis";
                bw.CancelAsync();
            }
        }

        private static string MimeType(string strFileName)
        {
            string retval = "";
            switch (System.IO.Path.GetExtension(strFileName).ToLower())
            {
                case ".3dm": retval = "x-world/x-3dmf"; break;
                case ".3dmf": retval = "x-world/x-3dmf"; break;
                case ".a": retval = "application/octet-stream"; break;
                case ".aab": retval = "application/x-authorware-bin"; break;
                case ".aam": retval = "application/x-authorware-map"; break;
                case ".aas": retval = "application/x-authorware-seg"; break;
                case ".abc": retval = "text/vnd.abc"; break;
                case ".acgi": retval = "text/html"; break;
                case ".afl": retval = "video/animaflex"; break;
                case ".ai": retval = "application/postscript"; break;
                case ".aif": retval = "audio/aiff"; break;
                case ".aifc": retval = "audio/aiff"; break;
                case ".aiff": retval = "audio/aiff"; break;
                case ".aim": retval = "application/x-aim"; break;
                case ".aip": retval = "text/x-audiosoft-intra"; break;
                case ".ani": retval = "application/x-navi-animation"; break;
                case ".aos": retval = "application/x-nokia-9000-communicator-add-on-software"; break;
                case ".aps": retval = "application/mime"; break;
                case ".arc": retval = "application/octet-stream"; break;
                case ".arj": retval = "application/arj"; break;
                case ".art": retval = "image/x-jg"; break;
                case ".asf": retval = "video/x-ms-asf"; break;
                case ".asm": retval = "text/x-asm"; break;
                case ".asp": retval = "text/asp"; break;
                case ".asx": retval = "video/x-ms-asf"; break;
                case ".au": retval = "audio/basic"; break;
                case ".avi": retval = "video/avi"; break;
                case ".avs": retval = "video/avs-video"; break;
                case ".bcpio": retval = "application/x-bcpio"; break;
                case ".bin": retval = "application/octet-stream"; break;
                case ".bm": retval = "image/bmp"; break;
                case ".bmp": retval = "image/bmp"; break;
                case ".boo": retval = "application/book"; break;
                case ".book": retval = "application/book"; break;
                case ".boz": retval = "application/x-bzip2"; break;
                case ".bsh": retval = "application/x-bsh"; break;
                case ".bz": retval = "application/x-bzip"; break;
                case ".bz2": retval = "application/x-bzip2"; break;
                case ".c": retval = "text/plain"; break;
                case ".c++": retval = "text/plain"; break;
                case ".cat": retval = "application/vnd.ms-pki.seccat"; break;
                case ".cc": retval = "text/plain"; break;
                case ".ccad": retval = "application/clariscad"; break;
                case ".cco": retval = "application/x-cocoa"; break;
                case ".cdf": retval = "application/cdf"; break;
                case ".cer": retval = "application/pkix-cert"; break;
                case ".cha": retval = "application/x-chat"; break;
                case ".chat": retval = "application/x-chat"; break;
                case ".class": retval = "application/java"; break;
                case ".com": retval = "application/octet-stream"; break;
                case ".conf": retval = "text/plain"; break;
                case ".cpio": retval = "application/x-cpio"; break;
                case ".cpp": retval = "text/x-c"; break;
                case ".cpt": retval = "application/x-cpt"; break;
                case ".crl": retval = "application/pkcs-crl"; break;
                case ".crt": retval = "application/pkix-cert"; break;
                case ".csh": retval = "application/x-csh"; break;
                case ".css": retval = "text/css"; break;
                case ".cxx": retval = "text/plain"; break;
                case ".dcr": retval = "application/x-director"; break;
                case ".deepv": retval = "application/x-deepv"; break;
                case ".def": retval = "text/plain"; break;
                case ".der": retval = "application/x-x509-ca-cert"; break;
                case ".dif": retval = "video/x-dv"; break;
                case ".dir": retval = "application/x-director"; break;
                case ".dl": retval = "video/dl"; break;
                case ".doc": retval = "application/msword"; break;
                //case ".doc": retval = "text/rtf"; break;
                case ".dot": retval = "application/msword"; break;
                case ".dp": retval = "application/commonground"; break;
                case ".drw": retval = "application/drafting"; break;
                case ".dump": retval = "application/octet-stream"; break;
                case ".dv": retval = "video/x-dv"; break;
                case ".dvi": retval = "application/x-dvi"; break;
                case ".dwf": retval = "model/vnd.dwf"; break;
                case ".dwg": retval = "image/vnd.dwg"; break;
                case ".dxf": retval = "image/vnd.dwg"; break;
                case ".dxr": retval = "application/x-director"; break;
                case ".el": retval = "text/x-script.elisp"; break;
                case ".elc": retval = "application/x-elc"; break;
                case ".env": retval = "application/x-envoy"; break;
                case ".eps": retval = "application/postscript"; break;
                case ".es": retval = "application/x-esrehber"; break;
                case ".etx": retval = "text/x-setext"; break;
                case ".evy": retval = "application/envoy"; break;
                case ".exe": retval = "application/octet-stream"; break;
                case ".f": retval = "text/plain"; break;
                case ".f77": retval = "text/x-fortran"; break;
                case ".f90": retval = "text/plain"; break;
                case ".fdf": retval = "application/vnd.fdf"; break;
                case ".fif": retval = "image/fif"; break;
                case ".fli": retval = "video/fli"; break;
                case ".flo": retval = "image/florian"; break;
                case ".flx": retval = "text/vnd.fmi.flexstor"; break;
                case ".fmf": retval = "video/x-atomic3d-feature"; break;
                case ".for": retval = "text/x-fortran"; break;
                case ".fpx": retval = "image/vnd.fpx"; break;
                case ".frl": retval = "application/freeloader"; break;
                case ".funk": retval = "audio/make"; break;
                case ".g": retval = "text/plain"; break;
                case ".g3": retval = "image/g3fax"; break;
                case ".gif": retval = "image/gif"; break;
                case ".gl": retval = "video/gl"; break;
                case ".gsd": retval = "audio/x-gsm"; break;
                case ".gsm": retval = "audio/x-gsm"; break;
                case ".gsp": retval = "application/x-gsp"; break;
                case ".gss": retval = "application/x-gss"; break;
                case ".gtar": retval = "application/x-gtar"; break;
                case ".gz": retval = "application/x-gzip"; break;
                case ".gzip": retval = "application/x-gzip"; break;
                case ".h": retval = "text/plain"; break;
                case ".hdf": retval = "application/x-hdf"; break;
                case ".help": retval = "application/x-helpfile"; break;
                case ".hgl": retval = "application/vnd.hp-hpgl"; break;
                case ".hh": retval = "text/plain"; break;
                case ".hlb": retval = "text/x-script"; break;
                case ".hlp": retval = "application/hlp"; break;
                case ".hpg": retval = "application/vnd.hp-hpgl"; break;
                case ".hpgl": retval = "application/vnd.hp-hpgl"; break;
                case ".hqx": retval = "application/binhex"; break;
                case ".hta": retval = "application/hta"; break;
                case ".htc": retval = "text/x-component"; break;
                case ".htm": retval = "text/html"; break;
                case ".html": retval = "text/html"; break;
                case ".htmls": retval = "text/html"; break;
                case ".htt": retval = "text/webviewhtml"; break;
                case ".htx": retval = "text/html"; break;
                case ".ice": retval = "x-conference/x-cooltalk"; break;
                case ".ico": retval = "image/x-icon"; break;
                case ".idc": retval = "text/plain"; break;
                case ".ief": retval = "image/ief"; break;
                case ".iefs": retval = "image/ief"; break;
                case ".iges": retval = "application/iges"; break;
                case ".igs": retval = "application/iges"; break;
                case ".ima": retval = "application/x-ima"; break;
                case ".imap": retval = "application/x-httpd-imap"; break;
                case ".inf": retval = "application/inf"; break;
                case ".ins": retval = "application/x-internett-signup"; break;
                case ".ip": retval = "application/x-ip2"; break;
                case ".isu": retval = "video/x-isvideo"; break;
                case ".it": retval = "audio/it"; break;
                case ".iv": retval = "application/x-inventor"; break;
                case ".ivr": retval = "i-world/i-vrml"; break;
                case ".ivy": retval = "application/x-livescreen"; break;
                case ".jam": retval = "audio/x-jam"; break;
                case ".jav": retval = "text/plain"; break;
                case ".java": retval = "text/plain"; break;
                case ".jcm": retval = "application/x-java-commerce"; break;
                case ".jfif": retval = "image/jpeg"; break;
                case ".jfif-tbnl": retval = "image/jpeg"; break;
                case ".jpe": retval = "image/jpeg"; break;
                case ".jpeg": retval = "image/jpeg"; break;
                case ".jpg": retval = "image/jpeg"; break;
                case ".jps": retval = "image/x-jps"; break;
                case ".js": retval = "application/x-javascript"; break;
                case ".jut": retval = "image/jutvision"; break;
                case ".kar": retval = "audio/midi"; break;
                case ".ksh": retval = "application/x-ksh"; break;
                case ".la": retval = "audio/nspaudio"; break;
                case ".lam": retval = "audio/x-liveaudio"; break;
                case ".latex": retval = "application/x-latex"; break;
                case ".lha": retval = "application/octet-stream"; break;
                case ".lhx": retval = "application/octet-stream"; break;
                case ".list": retval = "text/plain"; break;
                case ".lma": retval = "audio/nspaudio"; break;
                case ".log": retval = "text/plain"; break;
                case ".lsp": retval = "application/x-lisp"; break;
                case ".lst": retval = "text/plain"; break;
                case ".lsx": retval = "text/x-la-asf"; break;
                case ".ltx": retval = "application/x-latex"; break;
                case ".lzh": retval = "application/octet-stream"; break;
                case ".lzx": retval = "application/octet-stream"; break;
                case ".m": retval = "text/plain"; break;
                case ".m1v": retval = "video/mpeg"; break;
                case ".m2a": retval = "audio/mpeg"; break;
                case ".m2v": retval = "video/mpeg"; break;
                case ".m3u": retval = "audio/x-mpequrl"; break;
                case ".man": retval = "application/x-troff-man"; break;
                case ".map": retval = "application/x-navimap"; break;
                case ".mar": retval = "text/plain"; break;
                case ".mbd": retval = "application/mbedlet"; break;
                case ".mc$": retval = "application/x-magic-cap-package-1.0"; break;
                case ".mcd": retval = "application/mcad"; break;
                case ".mcf": retval = "text/mcf"; break;
                case ".mcp": retval = "application/netmc"; break;
                case ".me": retval = "application/x-troff-me"; break;
                case ".mht": retval = "message/rfc822"; break;
                case ".mhtml": retval = "message/rfc822"; break;
                case ".mid": retval = "audio/midi"; break;
                case ".midi": retval = "audio/midi"; break;
                case ".mif": retval = "application/x-mif"; break;
                case ".mime": retval = "message/rfc822"; break;
                case ".mjf": retval = "audio/x-vnd.audioexplosion.mjuicemediafile"; break;
                case ".mjpg": retval = "video/x-motion-jpeg"; break;
                case ".mm": retval = "application/base64"; break;
                case ".mme": retval = "application/base64"; break;
                case ".mod": retval = "audio/mod"; break;
                case ".moov": retval = "video/quicktime"; break;
                case ".mov": retval = "video/quicktime"; break;
                case ".movie": retval = "video/x-sgi-movie"; break;
                case ".mp2": retval = "audio/mpeg"; break;
                case ".mp3": retval = "audio/mpeg"; break;
                case ".mpa": retval = "audio/mpeg"; break;
                case ".mpc": retval = "application/x-project"; break;
                case ".mpe": retval = "video/mpeg"; break;
                case ".mpeg": retval = "video/mpeg"; break;
                case ".mpg": retval = "video/mpeg"; break;
                case ".mpga": retval = "audio/mpeg"; break;
                case ".mpp": retval = "application/vnd.ms-project"; break;
                case ".mpt": retval = "application/vnd.ms-project"; break;
                case ".mpv": retval = "application/vnd.ms-project"; break;
                case ".mpx": retval = "application/vnd.ms-project"; break;
                case ".mrc": retval = "application/marc"; break;
                case ".ms": retval = "application/x-troff-ms"; break;
                case ".mv": retval = "video/x-sgi-movie"; break;
                case ".my": retval = "audio/make"; break;
                case ".mzz": retval = "application/x-vnd.audioexplosion.mzz"; break;
                case ".nap": retval = "image/naplps"; break;
                case ".naplps": retval = "image/naplps"; break;
                case ".nc": retval = "application/x-netcdf"; break;
                case ".ncm": retval = "application/vnd.nokia.configuration-message"; break;
                case ".nif": retval = "image/x-niff"; break;
                case ".niff": retval = "image/x-niff"; break;
                case ".nix": retval = "application/x-mix-transfer"; break;
                case ".nsc": retval = "application/x-conference"; break;
                case ".nvd": retval = "application/x-navidoc"; break;
                case ".o": retval = "application/octet-stream"; break;
                case ".oda": retval = "application/oda"; break;
                case ".omc": retval = "application/x-omc"; break;
                case ".omcd": retval = "application/x-omcdatamaker"; break;
                case ".omcr": retval = "application/x-omcregerator"; break;
                case ".p": retval = "text/x-pascal"; break;
                case ".p10": retval = "application/pkcs10"; break;
                case ".p12": retval = "application/pkcs-12"; break;
                case ".p7a": retval = "application/x-pkcs7-signature"; break;
                case ".p7c": retval = "application/pkcs7-mime"; break;
                case ".p7m": retval = "application/pkcs7-mime"; break;
                case ".p7r": retval = "application/x-pkcs7-certreqresp"; break;
                case ".p7s": retval = "application/pkcs7-signature"; break;
                case ".part": retval = "application/pro_eng"; break;
                case ".pas": retval = "text/pascal"; break;
                case ".pbm": retval = "image/x-portable-bitmap"; break;
                case ".pcl": retval = "application/vnd.hp-pcl"; break;
                case ".pct": retval = "image/x-pict"; break;
                case ".pcx": retval = "image/x-pcx"; break;
                case ".pdb": retval = "chemical/x-pdb"; break;
                case ".pdf": retval = "application/pdf"; break;
                case ".pfunk": retval = "audio/make"; break;
                case ".pgm": retval = "image/x-portable-greymap"; break;
                case ".pic": retval = "image/pict"; break;
                case ".pict": retval = "image/pict"; break;
                case ".pkg": retval = "application/x-newton-compatible-pkg"; break;
                case ".pko": retval = "application/vnd.ms-pki.pko"; break;
                case ".pl": retval = "text/plain"; break;
                case ".plx": retval = "application/x-pixclscript"; break;
                case ".pm": retval = "image/x-xpixmap"; break;
                case ".pm4": retval = "application/x-pagemaker"; break;
                case ".pm5": retval = "application/x-pagemaker"; break;
                case ".png": retval = "image/png"; break;
                case ".pnm": retval = "application/x-portable-anymap"; break;
                case ".pot": retval = "application/vnd.ms-powerpoint"; break;
                case ".pov": retval = "model/x-pov"; break;
                case ".ppa": retval = "application/vnd.ms-powerpoint"; break;
                case ".ppm": retval = "image/x-portable-pixmap"; break;
                case ".pps": retval = "application/vnd.ms-powerpoint"; break;
                case ".ppt": retval = "application/vnd.ms-powerpoint"; break;
                case ".ppz": retval = "application/vnd.ms-powerpoint"; break;
                case ".pre": retval = "application/x-freelance"; break;
                case ".prt": retval = "application/pro_eng"; break;
                case ".ps": retval = "application/postscript"; break;
                case ".psd": retval = "application/octet-stream"; break;
                case ".pvu": retval = "paleovu/x-pv"; break;
                case ".pwz": retval = "application/vnd.ms-powerpoint"; break;
                case ".py": retval = "text/x-script.phyton"; break;
                case ".pyc": retval = "applicaiton/x-bytecode.python"; break;
                case ".qcp": retval = "audio/vnd.qcelp"; break;
                case ".qd3": retval = "x-world/x-3dmf"; break;
                case ".qd3d": retval = "x-world/x-3dmf"; break;
                case ".qif": retval = "image/x-quicktime"; break;
                case ".qt": retval = "video/quicktime"; break;
                case ".qtc": retval = "video/x-qtc"; break;
                case ".qti": retval = "image/x-quicktime"; break;
                case ".qtif": retval = "image/x-quicktime"; break;
                case ".ra": retval = "audio/x-pn-realaudio"; break;
                case ".ram": retval = "audio/x-pn-realaudio"; break;
                case ".ras": retval = "application/x-cmu-raster"; break;
                case ".rast": retval = "image/cmu-raster"; break;
                case ".rexx": retval = "text/x-script.rexx"; break;
                case ".rf": retval = "image/vnd.rn-realflash"; break;
                case ".rgb": retval = "image/x-rgb"; break;
                case ".rm": retval = "application/vnd.rn-realmedia"; break;
                case ".rmi": retval = "audio/mid"; break;
                case ".rmm": retval = "audio/x-pn-realaudio"; break;
                case ".rmp": retval = "audio/x-pn-realaudio"; break;
                case ".rng": retval = "application/ringing-tones"; break;
                case ".rnx": retval = "application/vnd.rn-realplayer"; break;
                case ".roff": retval = "application/x-troff"; break;
                case ".rp": retval = "image/vnd.rn-realpix"; break;
                case ".rpm": retval = "audio/x-pn-realaudio-plugin"; break;
                case ".rt": retval = "text/richtext"; break;
                case ".rtf": retval = "text/rtf"; break;
                case ".rtx": retval = "text/richtext"; break;
                case ".rv": retval = "video/vnd.rn-realvideo"; break;
                case ".s": retval = "text/x-asm"; break;
                case ".s3m": retval = "audio/s3m"; break;
                case ".saveme": retval = "application/octet-stream"; break;
                case ".sbk": retval = "application/x-tbook"; break;
                case ".scm": retval = "application/x-lotusscreencam"; break;
                case ".sdml": retval = "text/plain"; break;
                case ".sdp": retval = "application/sdp"; break;
                case ".sdr": retval = "application/sounder"; break;
                case ".sea": retval = "application/sea"; break;
                case ".set": retval = "application/set"; break;
                case ".sgm": retval = "text/sgml"; break;
                case ".sgml": retval = "text/sgml"; break;
                case ".sh": retval = "application/x-sh"; break;
                case ".shar": retval = "application/x-shar"; break;
                case ".shtml": retval = "text/html"; break;
                case ".sid": retval = "audio/x-psid"; break;
                case ".sit": retval = "application/x-sit"; break;
                case ".skd": retval = "application/x-koan"; break;
                case ".skm": retval = "application/x-koan"; break;
                case ".skp": retval = "application/x-koan"; break;
                case ".skt": retval = "application/x-koan"; break;
                case ".sl": retval = "application/x-seelogo"; break;
                case ".smi": retval = "application/smil"; break;
                case ".smil": retval = "application/smil"; break;
                case ".snd": retval = "audio/basic"; break;
                case ".sol": retval = "application/solids"; break;
                case ".spc": retval = "text/x-speech"; break;
                case ".spl": retval = "application/futuresplash"; break;
                case ".spr": retval = "application/x-sprite"; break;
                case ".sprite": retval = "application/x-sprite"; break;
                case ".src": retval = "application/x-wais-source"; break;
                case ".ssi": retval = "text/x-server-parsed-html"; break;
                case ".ssm": retval = "application/streamingmedia"; break;
                case ".sst": retval = "application/vnd.ms-pki.certstore"; break;
                case ".step": retval = "application/step"; break;
                case ".stl": retval = "application/sla"; break;
                case ".stp": retval = "application/step"; break;
                case ".sv4cpio": retval = "application/x-sv4cpio"; break;
                case ".sv4crc": retval = "application/x-sv4crc"; break;
                case ".svf": retval = "image/vnd.dwg"; break;
                case ".svr": retval = "application/x-world"; break;
                case ".swf": retval = "application/x-shockwave-flash"; break;
                case ".t": retval = "application/x-troff"; break;
                case ".talk": retval = "text/x-speech"; break;
                case ".tar": retval = "application/x-tar"; break;
                case ".tbk": retval = "application/toolbook"; break;
                case ".tcl": retval = "application/x-tcl"; break;
                case ".tcsh": retval = "text/x-script.tcsh"; break;
                case ".tex": retval = "application/x-tex"; break;
                case ".texi": retval = "application/x-texinfo"; break;
                case ".texinfo": retval = "application/x-texinfo"; break;
                case ".text": retval = "text/plain"; break;
                case ".tgz": retval = "application/x-compressed"; break;
                case ".tif": retval = "image/tiff"; break;
                case ".tiff": retval = "image/tiff"; break;
                case ".tr": retval = "application/x-troff"; break;
                case ".tsi": retval = "audio/tsp-audio"; break;
                case ".tsp": retval = "application/dsptype"; break;
                case ".tsv": retval = "text/tab-separated-values"; break;
                case ".turbot": retval = "image/florian"; break;
                case ".txt": retval = "text/plain"; break;
                case ".uil": retval = "text/x-uil"; break;
                case ".uni": retval = "text/uri-list"; break;
                case ".unis": retval = "text/uri-list"; break;
                case ".unv": retval = "application/i-deas"; break;
                case ".uri": retval = "text/uri-list"; break;
                case ".uris": retval = "text/uri-list"; break;
                case ".ustar": retval = "application/x-ustar"; break;
                case ".uu": retval = "application/octet-stream"; break;
                case ".uue": retval = "text/x-uuencode"; break;
                case ".vcd": retval = "application/x-cdlink"; break;
                case ".vcs": retval = "text/x-vcalendar"; break;
                case ".vda": retval = "application/vda"; break;
                case ".vdo": retval = "video/vdo"; break;
                case ".vew": retval = "application/groupwise"; break;
                case ".viv": retval = "video/vivo"; break;
                case ".vivo": retval = "video/vivo"; break;
                case ".vmd": retval = "application/vocaltec-media-desc"; break;
                case ".vmf": retval = "application/vocaltec-media-file"; break;
                case ".voc": retval = "audio/voc"; break;
                case ".vos": retval = "video/vosaic"; break;
                case ".vox": retval = "audio/voxware"; break;
                case ".vqe": retval = "audio/x-twinvq-plugin"; break;
                case ".vqf": retval = "audio/x-twinvq"; break;
                case ".vql": retval = "audio/x-twinvq-plugin"; break;
                case ".vrml": retval = "application/x-vrml"; break;
                case ".vrt": retval = "x-world/x-vrt"; break;
                case ".vsd": retval = "application/x-visio"; break;
                case ".vst": retval = "application/x-visio"; break;
                case ".vsw": retval = "application/x-visio"; break;
                case ".w60": retval = "application/wordperfect6.0"; break;
                case ".w61": retval = "application/wordperfect6.1"; break;
                case ".w6w": retval = "application/msword"; break;
                case ".wav": retval = "audio/wav"; break;
                case ".wb1": retval = "application/x-qpro"; break;
                case ".wbmp": retval = "image/vnd.wap.wbmp"; break;
                case ".web": retval = "application/vnd.xara"; break;
                case ".wiz": retval = "application/msword"; break;
                case ".wk1": retval = "application/x-123"; break;
                case ".wmf": retval = "windows/metafile"; break;
                case ".wml": retval = "text/vnd.wap.wml"; break;
                case ".wmlc": retval = "application/vnd.wap.wmlc"; break;
                case ".wmls": retval = "text/vnd.wap.wmlscript"; break;
                case ".wmlsc": retval = "application/vnd.wap.wmlscriptc"; break;
                case ".word": retval = "application/msword"; break;
                case ".wp": retval = "application/wordperfect"; break;
                case ".wp5": retval = "application/wordperfect"; break;
                case ".wp6": retval = "application/wordperfect"; break;
                case ".wpd": retval = "application/wordperfect"; break;
                case ".wq1": retval = "application/x-lotus"; break;
                case ".wri": retval = "application/mswrite"; break;
                case ".wrl": retval = "application/x-world"; break;
                case ".wrz": retval = "x-world/x-vrml"; break;
                case ".wsc": retval = "text/scriplet"; break;
                case ".wsrc": retval = "application/x-wais-source"; break;
                case ".wtk": retval = "application/x-wintalk"; break;
                case ".xbm": retval = "image/x-xbitmap"; break;
                case ".xdr": retval = "video/x-amt-demorun"; break;
                case ".xgz": retval = "xgl/drawing"; break;
                case ".xif": retval = "image/vnd.xiff"; break;
                case ".xl": retval = "application/excel"; break;
                case ".xla": retval = "application/vnd.ms-excel"; break;
                case ".xlb": retval = "application/vnd.ms-excel"; break;
                case ".xlc": retval = "application/vnd.ms-excel"; break;
                case ".xld": retval = "application/vnd.ms-excel"; break;
                case ".xlk": retval = "application/vnd.ms-excel"; break;
                case ".xll": retval = "application/vnd.ms-excel"; break;
                case ".xlm": retval = "application/vnd.ms-excel"; break;
                case ".xls": retval = "application/vnd.ms-excel"; break;
                case ".xlt": retval = "application/vnd.ms-excel"; break;
                case ".xlv": retval = "application/vnd.ms-excel"; break;
                case ".xlw": retval = "application/vnd.ms-excel"; break;
                case ".xm": retval = "audio/xm"; break;
                case ".xml": retval = "application/xml"; break;
                case ".xmz": retval = "xgl/movie"; break;
                case ".xpix": retval = "application/x-vnd.ls-xpix"; break;
                case ".xpm": retval = "image/xpm"; break;
                case ".x-png": retval = "image/png"; break;
                case ".xsr": retval = "video/x-amt-showrun"; break;
                case ".xwd": retval = "image/x-xwd"; break;
                case ".xyz": retval = "chemical/x-pdb"; break;
                case ".z": retval = "application/x-compressed"; break;
                case ".zip": retval = "application/zip"; break;
                case ".zoo": retval = "application/octet-stream"; break;
                case ".zsh": retval = "text/x-script.zsh"; break;
                default: retval = "application/octet-stream"; break;
            }
            return retval;
        }
        public string lbValue = "";
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbValue = listBox1.SelectedValue.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK) {
                textBox5.Text = of.FileName;
                overSubor(of.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectSingleFile();
        }

        private void jednoduchýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.BringToFront();
        }

        private void multiPodpisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.BringToFront();
        }

        private void overSúborToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.BringToFront();
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void oProgrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox4.BringToFront();
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
        private void loadOProgrameText()
        {
            webBrowser1.DocumentText = "<html><head><title></title><style>body{background:#eeeeee;} h1{font-size:1.3em;} .important{color:blue;font-weight:bold;}</style></head><body><h1>Fast Zep 3</h1><p>Fast Zep 3 je open source program vytvorený pre vytváranie zaručených elektronických podpisov. Jeho možnosti siahajú aj cez hranice vytvárania zaručených podpisov, a umožňuje vytvárať a podpisovať dokumenty ktoré podľa zákona nespadajú pod zaručený elektronický podpis.</p><p>Hlavné výhody využívania FastZep3 sú: <ul><li>Fast Zep je <span class=\"important\">zadarmo</span></li><li>Fast Zep je <span class=\"important\">Open source</span></li><li>Možnosť podpisovať dokumenty <span class=\"important\">ľubovoľným certifikátom</span> (zaručeným alebo nie)</li><li>Možnosť podpisovať <span class=\"important\">ľubovoľný súbor</span>: PDF, DOC, XSL, RTF, PNG, TIF, JPG, EXE, ZIP, RAR, ...</li></ul></p><p>Aj keď môžete využívať FastZep úplne zadarmo, vývoj stojí peniaze. Ak chcete finančne pomôcť a umožniť tak ďalší vývoj programu, prispejte, alebo sa zúčastnite školenia, alebo si objednajte integrovanú implementáciu pre Váš podnikový informačný systém.</body></html>";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (signFile(textBox1.Text, textBox2.Text, comboBox2.SelectedValue.ToString(),checkBox1.Checked))
            {
                FZRegistry.set("lastSignFileInput",textBox1.Text);
                FZRegistry.set("lastSignFileOutput",textBox2.Text);
                MessageBox.Show("Podpísané");
            }
            else
            {
                MessageBox.Show("Nastala chyba pri podpisovaní. Súbor nebol podpísaný.","Chyba pri podpisovaní",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void selectSingleFile()
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = of.FileName;
                textBox2.Text = of.FileName + ".zep";
            }
        }

        private void selectSingleFileOutput()
        {
            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "Zaručený elektronický podpis (*.zep)|*.zep";
            if (of.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = of.FileName;
            }
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            selectSingleFile();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            //selectSingleFile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectSingleFileOutput();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            //selectSingleFileOutput();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { return; }
            try
            {
                Process.Start(textBox1.Text);
            }
            catch (Exception exc) {
                MessageBox.Show("Nastala chyba pri kontrole súboru! "+ exc.Message, "Chyba",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void selectAddrInput()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                folderBrowserDialog2.SelectedPath = textBox3.Text + @"\zep";
                textBox4.Text = folderBrowserDialog2.SelectedPath;
            }
        }
        private void selectAddrOutput()
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog2.SelectedPath;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            selectAddrInput();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            selectAddrOutput();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") { return; }
            try
            {
                Process.Start(textBox3.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nastala chyba pri kontrole súboru! " + exc.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmpadr = FastZep.FastZepFolder + "verify";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                if (row.Cells.Count < 4) return;
                string file = tmpadr+@"\S" + row.Cells[0].Value + ".cer";
                if(File.Exists(file)){
                    Process.Start(file);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string tmpadr = FastZep.FastZepFolder + @"verify";
            string file = tmpadr+@"\"+verifiedFileName;
            if(File.Exists(file)){
                Process.Start(file);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tmpadr = FastZep.FastZepFolder + "verify";
            string file = tmpadr + @"\" + verifiedFileName;
            if (!File.Exists(file)) { MessageBox.Show("Nepodarilo sa získať súbor", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            SaveFileDialog sd = new SaveFileDialog();
            if (sd.ShowDialog() == DialogResult.OK) {
                File.WriteAllBytes(sd.FileName, File.ReadAllBytes(file));
                MessageBox.Show("Ok");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?hosted_button_id=M6YUSCUR53KM6&cmd=_s-xclick");
        }

        private void obToolStripMenuItem_Click(object sender, EventArgs e)
        {

            reloadCerts();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            showOnlyZEPCertificates = true;
            reloadCerts();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            showOnlyZEPCertificates = false;
            reloadCerts();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Directory.Delete(FastZep.FastZepFolder, true);
                bw.Dispose();
                
            }
            catch{ }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedValue != null) { 
                string file = FastZep.FastZepFolder + listBox1.SelectedValue +".cer";
                if (File.Exists(file)) {
                    Process.Start(file);
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            showOnlyZEPCertificates = true;
            reloadCerts();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            showOnlyZEPCertificates = false;
            reloadCerts();
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedValue != null)
            {
                string file = FastZep.FastZepFolder + listBox2.SelectedValue + ".cer";
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
            }
        }

        private void uložDoSúboruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmpadr = FastZep.FastZepFolder + "verify";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells.Count < 4) return;
                string file = tmpadr + @"\S" + row.Cells[0].Value + ".cer";
                if (!File.Exists(file)) {
                    file = tmpadr + @"\U" + row.Cells[0].Value + ".cer";
                }
                if (File.Exists(file))
                {
                    SaveFileDialog save = new SaveFileDialog();
                    if (save.ShowDialog() == DialogResult.OK) {
                        File.WriteAllBytes(save.FileName, File.ReadAllBytes(file));
                    }
                }
                else
                {
                    MessageBox.Show("Nepodarilo sa uložiť" + @" S" + row.Cells[0].Value + ".cer");
                }
            }
        }

        private void zobrazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmpadr = FastZep.FastZepFolder + "verify";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells.Count < 4) return;
                string file = tmpadr + @"\S" + row.Cells[0].Value + ".cer";
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
                else {
                    MessageBox.Show("Nepodarilo sa zobraziť" + @" S" + row.Cells[0].Value + ".cer");
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void groupBoxPolicy_Enter(object sender, EventArgs e)
        {
        }

        private void podpisováPolitikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPolicy.BringToFront();
        }

        private void dataGridViewPolitiky_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private byte[] createShortPolicy(byte[] der,bool trim) {
            try
            {
                File.WriteAllBytes("d:/policy.der", der);

                var dercorelist = ASNNode.parse(der);
                ASNNode dercore = (ASNNode) dercorelist[0];
                
                
                ASNNode seq1;
                ASNNode seq2;
                ASNNode seq3;
                ASNNode seq4;
                ASNNode seq5;


                ASNNode ret = new ASNNode(AsnTag.SEQUENCE);
                ret.AppendChild(getPolicyId(dercore));
                seq1 = new ASNNode(AsnTag.SEQUENCE);
                seq2 = new ASNNode(AsnTag.SEQUENCE);
                seq2.AppendChild(new ASNNode(new Oid("2.16.840.1.101.3.4.2.1")));
                seq1.AppendChild(seq2);
                seq1.AppendChild(getPolicyHash(dercore));
                ret.AppendChild(seq1);


                seq1 = new ASNNode(AsnTag.SEQUENCE);
                seq2 = new ASNNode(AsnTag.SEQUENCE);
                seq2.AppendChild(new ASNNode(new Oid("1.2.840.113549.1.9.16.5.2")));
                seq3 = new ASNNode(AsnTag.SEQUENCE);

                seq4 = new ASNNode(AsnTag.SEQUENCE);
                string vydavatel = getPolicyVydavatel(dercore);
                //vydavatel = "test";
                //vydavatel = "C=SK, L=Bratislava, O=Narodny bezpecnostny urad, OU=Sekcia IBEP";
                //vydavatel = "C=SK, L=Bratislava, O=Narodny bezpecnostny urad, OU=Sekcia IBEA";
                seq4.AppendChild(new ASNNode(vydavatel, AsnTag.UTF8_STRING));

                //seq3.AppendChild(new ASNNode(Encoding.UTF8.GetBytes(getPolicyVydavatel(dercore)), AsnTag.UTF8_STRING));
                //seq3.AppendChild(new ASNNode(Encoding.UTF8.GetBytes(getPolicyVydavatel(dercore)), AsnTag.UTF8_STRING));
                
                seq5 = new ASNNode(AsnTag.SEQUENCE);
                seq5.AppendChild(new ASNNode(1));
                seq4.AppendChild(seq5);
                /**/
                
                seq3.AppendChild(seq4);
                seq3.AppendChild(getPolicyUsage(dercore));
                seq2.AppendChild(seq3);

                seq1.AppendChild(seq2);



                seq2 = new ASNNode(AsnTag.SEQUENCE);
                seq2.AppendChild(new ASNNode(new Oid("1.2.840.113549.1.9.16.5.1")));
                seq2.AppendChild(new ASNNode(getPolicyUri(dercore).getValue(),AsnTag.IA5_STRING));
                seq1.AppendChild(seq2);
                /**/
                ret.AppendChild(seq1);
                if (!trim) return ret.get();
                byte[] policy = ret.get();
                byte[] policy2 = new byte[policy.Length - 4];
                Buffer.BlockCopy(policy, 4, policy2, 0, policy2.Length);
                return policy2;
            }
            catch (Exception exc) {
                throw new Exception("Nepodarilo sa vytvoriť súbor policy. "+exc.Message);
            }
        }
        
        private ASNNode getPolicyUsage(ASNNode der)
        {
            return der.getChilds()[1].getChilds()[3];
        }
        private ASNNode getPolicyId(ASNNode der)
        {
            return der.getChilds()[1].getChilds()[0];
        }
        private ASNNode getPolicyHash(ASNNode der)
        {
            return der.getChilds()[der.getChilds().Length - 1];
        }
        private ASNNode getPolicyUri(ASNNode der)
        {
            return der.getChilds()[1].getChilds()[2].getChilds()[1];
        }
        private string getPolicyVydavatel(ASNNode der)
        {
            string ret = "";

            string[,] convert = new string[,] {
                {"2.5.4.43","I="},
                {"2.5.4.42","G="},
                {"2.5.4.12","T="},
                {"2.5.4.11","OU="},
                {"2.5.4.10","O="},
                {"2.5.4.8","ST="},
                {"2.5.4.7","L="},
                {"2.5.4.6","C="},
                {"2.5.4.4","SN="},
                {"2.5.4.3","CN="}
            };

            for (int i = 0; i <= convert.GetUpperBound(0); i++) {
                string code = convert[i, 0];
                string add = convert[i, 1];

                foreach (var asn in der.getChilds()[1].getChilds()[2].getChilds()[0].getChilds()[0].getChilds())
                {
                    var id = asn.getChilds()[0].getChilds()[0].getValue();
                    var value = Encoding.UTF8.GetString(asn.getChilds()[0].getChilds()[1].getValue());
                    if (MyOid.getName(new Oid(code)).SequenceEqual(id))
                    {
                        if (ret != "") ret += ", ";
                        ret += add + value;
                    }
                }
            }
            return ret;
        }
        private void loadPolicy() {

            try
            {
                var client = new WebClient();
                byte[] data = null;

                try
                {
                    string trustedlistdata = FZRegistry.get("trustedlist.p7m");
                    if (trustedlistdata == null) throw new Exception("Dont have trusted list");
                    string[] trustedlistdataarr = trustedlistdata.Split(';');
                    data = Convert.FromBase64String(trustedlistdataarr[1]);
                    if (long.Parse(trustedlistdataarr[0]) < DateTime.Now.Ticks - (long) 24*3600*10000000) {
                        throw new Exception("Trusted list is too old");
                    }
                }
                catch{
                    byte[] data2 = client.DownloadData("http://www.nbusr.sk/ipublisher/files/nbusr.sk/sign_policy/trustedlist.p7m");
                    if (data2 != null)
                    {
                        try
                        {

                            data = data2;
                            FZRegistry.set("trustedlist.p7m", DateTime.Now.Ticks.ToString() + ";" + Convert.ToBase64String(data2));
                        }
                        catch(Exception exc){
                            MessageBox.Show("Chyba v stiahnutom súbore trustedlist.p7m: "+exc.Message);
                        }
                    }
                }
                SignedCms signedCms = new SignedCms();
                signedCms.Decode(data);


                var stream = new StreamReader(new MemoryStream(signedCms.ContentInfo.Content),System.Text.Encoding.UTF8);
                string line = null;
                byte[] file = null;
                string notafter = "";
                string oid = "";
                string FieldOfApplication = "";
                ASNNode der = null;
                string web = "";
                while ((line = stream.ReadLine()) != null)
                {
                    if (line.Substring(0, 5) == "FILE=") {
                        
                        try
                        {
                            web = line.Substring(5);
                            

                            
                            
                        }
                        catch (Exception exc) {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    if (line.Substring(0, 7) == "NOTICE=") {
                        foreach (string data1 in line.Substring(7).Split(',')) {
                            string data2 = data1;
                            data2 = data2.Trim();
                            if (data2.Substring(data2.Length - 8) == "NotAfter")
                            {
                                notafter = data2.Substring(0, data2.Length - 8).Trim();
                            }
                            if (data2.Substring(0, 4) == "OID=")
                            {
                                oid = data2.Substring(4).Trim();
                            }
                            if (data2.Substring(0, 19) == "FieldOfApplication=")
                            {
                                FieldOfApplication = data2.Substring(19).Trim();
                            }
                            
                        }


                        // spracuj zaznam

                        try
                        {
                            if (web == "") throw new Exception("Failed to parse trustedlist.p7m file");
                            string name = web.Substring(web.LastIndexOf('/') + 1);
                            string created = name.Substring(0, 8);
                            var crdate = DateTime.ParseExact(created, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            var todate = DateTime.ParseExact(notafter.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            if (crdate > DateTime.Now) continue;
                            if (todate < DateTime.Now) continue;

                            if (oid == "") {
                                oid = name.Substring(0, name.Length - 4);
                            }
                            if (oid == "") throw new Exception("Failed to parse trustedlist.p7m file. Missing oid.");
                            if (web.Substring(web.Length - ".der".Length) != ".der") continue;
                            

                            if (!FZRegistry.isSet(oid + ".short.der"))
                            {
                                file = client.DownloadData(web);
                                //File.WriteAllBytes("d:/policy/" + name, file);
                                FZRegistry.set(oid+".der", Convert.ToBase64String(file));
                                //byte[] policy = createShortPolicy(file, false);
                                //FZRegistry.set(oid + ".short.der", Convert.ToBase64String(policy));
                                //File.WriteAllBytes("d:/policy/" + name + ".short.asn", policy);
                            }
                            else
                            {
                                //file = File.ReadAllBytes("d:/policy/" + name);
                                //byte[] policy = createShortPolicy(file, false);
                                //File.WriteAllBytes("d:/policy/" + name + ".short.asn", policy);
                            }
                            dataGridViewPolitiky.Rows.Add(new object[] { oid, FieldOfApplication, crdate.ToString("dd.mm.yyyy"), todate.ToString("dd.mm.yyyy") });


                            web = "";
                            oid = "";
                            FieldOfApplication = "";
                            notafter = "";

                        }
                        catch (Exception exc) {
                            MessageBox.Show(exc.Message);
                        }
                    }
                }
                    //File.WriteAllBytes("d:/test.txt", signedCms.ContentInfo.Content);
                //File.ReadAllLines()


            }
            catch (Exception exc) {
                MessageBox.Show("Chyba pri čítaní politiky: " + exc.Message);
            }

            //dataGridViewPolitiky.Rows.Add(new object[] { "policy1", "Zaručený elektronický podpis v súlade s legislatívou Slovenskej republiky.", "1.2.2010", "31.12.2010" });
            //dataGridViewPolitiky.Rows.Add(new object[] { "policy2", "Podpisová politika pre dokumenty podpísané ZEP v orgánoch štátnej správy.", "23.8.2010", "31.1.2014" });

            try
            {
                
                string sel = FZRegistry.get("savedPolicy");
                foreach(DataGridViewRow row in dataGridViewPolitiky.Rows){
                    if (row.Cells["id"].Value.ToString() == sel) row.Selected = true;
                }
            }
            catch { }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try{
                FZRegistry.set("savedPolicy", dataGridViewPolitiky.SelectedRows[0].Cells["id"].Value.ToString());
                MessageBox.Show("OK");
            }catch(Exception exc){
                MessageBox.Show("Chyba pri ukladani politiky: "+ exc.Message,"Chyba",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
    public class FastZep
    {
        public static string FastZepFolder
        {
            get
            {
                string fastzepfolder = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\FastZep\";
                if (!Directory.Exists(fastzepfolder)) Directory.CreateDirectory(fastzepfolder);
                return fastzepfolder;
            }
        }
    }
}