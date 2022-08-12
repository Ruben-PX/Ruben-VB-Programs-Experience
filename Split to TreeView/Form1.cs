using System.Linq;

namespace Split_to_TreeView {
    public partial class Form1:Form {
        public Form1() {
            InitializeComponent();
        }

        private String[] Data;
        private String FilePath = "";
        private String splitChar = "\\";

        private Dictionary<String, TreeNode> dict;
        private TreeNode fileTreeNode;

        private void button1_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                FilePath = openFileDialog1.FileName;
                textBox_FilePath.Text = FilePath;
                btn_ParseFile.Enabled = true;
            }
        }

        private void btn_ParseFile_Click(object sender, EventArgs e) {
            if (FilePath != "") {
                Data = File.ReadAllLines(FilePath);
                progressBar1.Maximum = Data.Length;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            int progress = 0;
            fileTreeNode = new TreeNode();
            dict = new Dictionary<string, TreeNode>();

            string last = "";
            foreach (string item in Data) {
                List<string>  dataSplit = item.Split(splitChar).ToList();

                for (int i = 0; i < dataSplit.Count; i++) {

                    if (i == 0) {
                        if (!dict.Keys.Contains(dataSplit[0])) {
                            dict.Add(dataSplit[0], fileTreeNode.Nodes.Add(dataSplit[0]));
                        }
                        last = dataSplit[0];
                    } else {
                        string baseSplit = dataSplit.GetRange(0, i).Aggregate((a, b) => a + "/" + b);
                        if (!dict.ContainsKey(baseSplit)) { 
                            if (dict.ContainsKey(last)) {
                                dict.Add(baseSplit, dict[last].Nodes.Add(dataSplit[i]));
                            }
                        }
                        last = baseSplit;
                    }

                }

                progress++;
                backgroundWorker1.ReportProgress(progress);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(fileTreeNode);
            treeView1.EndUpdate();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}