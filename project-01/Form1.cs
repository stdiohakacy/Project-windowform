using project_01.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_01
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            FakeData();
            showMajor();
            showMajorDetail();
        }
        Dictionary<string, Major> database = new Dictionary<string, Major>();
        Student studentDetail = null;
        private void FakeData()
        {
            Major cntt = new Major() {
                code = "CNTT",
                name = "Khoa cong nghe thong tin",
            };

            Major dt = new Major()
            {
                code = "DT",
                name = "Khoa dien tu",
            };

            Major kt = new Major()
            {
                code = "KT",
                name = "Khoa kinh te",
            };

            Major luat = new Major()
            {
                code = "kl",
                name = "Khoa luat",
            };

            database.Add(cntt.code, cntt);
            database.Add(dt.code, dt);
            database.Add(kt.code, kt);
            database.Add(luat.code, luat);

            Class lopCNTT1 = new Class()
            {
                code = "cntt1",
                name = "Dai hoc tin hoc 1"
            };

            cntt.classes.Add(lopCNTT1.code, lopCNTT1);
            lopCNTT1.KhoaChuQuan = cntt;
             
            Class lopCNTT2 = new Class()
            {
                code = "cntt2",
                name = "Dai hoc tin hoc 2"
            };

            cntt.classes.Add(lopCNTT2.code, lopCNTT2); 
            lopCNTT2.KhoaChuQuan = cntt;

            Class lopLuat01 = new Class()
            {
                code = "luat1",
                name = "Dai hoc Luat 1"
            };
            luat.classes.Add(lopLuat01.code, lopLuat01);
            lopLuat01.KhoaChuQuan = luat;

            Class lopLuat02 = new Class()
            {
                code = "luat2",
                name = "Dai hoc Luat 2"
            };
            luat.classes.Add(lopLuat02.code, lopLuat02);
            lopLuat02.KhoaChuQuan = luat;

            Class lopLuat03 = new Class()
            {
                code = "luat3",
                name = "Dai hoc Luat 3"
            };
            luat.classes.Add(lopLuat03.code, lopLuat03);
            lopLuat03.KhoaChuQuan = luat;

            Student student01 = new Student()
            {
                code = "sv01",
                name = "Nguyen A",
                gender = true,
                birthday = new DateTime(1992, 2, 1)
            };

            lopCNTT1.students.Add(student01.code, student01);
            student01.LopChuQuan = lopCNTT1;
        }

        private void showMajor() {
            tvMajor.Nodes.Clear();
            foreach (KeyValuePair<string, Major> itemMajor in database)
            {
                Major major = itemMajor.Value;
                TreeNode nodeMajor = new TreeNode(major.name);
                nodeMajor.Tag = major;
                tvMajor.Nodes.Add(nodeMajor);

                foreach(KeyValuePair<string, Class> itemClass in major.classes)
                {
                    Class classInstance = itemClass.Value;
                    TreeNode nodeClass = new TreeNode(classInstance.name);
                    nodeClass.Tag = classInstance;
                    nodeMajor.Nodes.Add(nodeClass);
                }
            }
            tvMajor.ExpandAll();
        }

        private void tvMajor_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                Class classInstance = e.Node.Tag as Class;
                showStudentsByClass(classInstance);
            }
            else
            {
                lvStudent.Items.Clear();
            }
        }

        private void showStudentsByClass(Class classInstance)
        {
            lvStudent.Items.Clear();
            foreach (KeyValuePair<string, Student> itemStudent in classInstance.students)
            {
                Student student = itemStudent.Value;
                ListViewItem lvi = new ListViewItem(student.code);
                lvi.SubItems.Add(student.name);
                lvi.SubItems.Add(student.gender == false ? "Male" : "Female");
                if (student.birthday != null)
                    lvi.SubItems.Add(student.birthday.ToString("dd/MM/yyyy"));
                else
                    lvi.SubItems.Add(student.birthday.ToString("<....>"));
                lvStudent.Items.Add(lvi);
                lvi.Tag = student;
            }
        }
        private void showMajorDetail()
        {
            cboMajor.Items.Clear();
            foreach (KeyValuePair<string, Major> itemMajor in database)
            {
                Major major = itemMajor.Value;
                cboMajor.Items.Add(major);
            }
        }

        private void cboMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMajor.SelectedIndex == -1)
                return;
            Major major = cboMajor.SelectedItem as Major;
            showClasses(major);
            
        }

        private void showClasses(Major major)
        {
            cboClass.Items.Clear();
            foreach (KeyValuePair<string, Class> itemClass in major.classes)
            {
                Class classInstance = itemClass.Value;
                cboClass.Items.Add(classInstance);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboMajor.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose Major");
                return;
            }
            if (cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose class");
                return;
            }

            Student student = new Student();
            student.code = txtCode.Text;
            student.name = txtName.Text;
            student.gender = radFemale.Checked;
            Class classInstance = cboClass.SelectedItem as Class;
            student.LopChuQuan = classInstance;

            classInstance.students.Add(student.code, student);
            MessageBox.Show("Save successed!");
        }

        private void lvStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvStudent.SelectedItems.Count == 0)
                return;
            ListViewItem lvi = lvStudent.SelectedItems[0];
            Student student = lvi.Tag as Student;
            txtCode.Text = student.code;
            txtName.Text = student.name;
            if (student.gender)
                radFemale.Checked = true;
            else
                radMale.Checked = true;
            studentDetail = student;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (studentDetail != null)
            {
                Class classInstance = studentDetail.LopChuQuan;
                classInstance.students.Remove(studentDetail.code);
                MessageBox.Show("Remove successed!");
            }
        }
    }
}
