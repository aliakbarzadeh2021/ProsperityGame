using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProsperityGameWinApp2
{
    public partial class AddActivity : Form
    {
        private MongoRepository _mongoRepository;
        public DateTime StartDate;

        public AddActivity(MongoRepository mongoRepository, DateTime startDate)
        {
            _mongoRepository = mongoRepository;
            StartDate = startDate;
            InitializeComponent();
            Load();
        }

        public AddActivity()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var taskList = _mongoRepository.GetUserTasksList().OrderByDescending(i => i.Date);
            cbSelectTask.DataSource = taskList.ToList();
            cbSelectTask.DisplayMember = "title";
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            var activityTitle = txtActivityTitle.Text;
            var selectedTask = (UserTask)cbSelectTask.SelectedItem;
            var time = int.Parse(txtTime.Text);
            _mongoRepository.AddUserActivity(selectedTask, activityTitle, time);

            AddMoney(StartDate, 1000, activityTitle);
            this.Hide();
        }

        

        private void AddMoney(DateTime startDate, double money, string description)
        {
            _mongoRepository.AddMoney(startDate, money, description);
        }
    }
}
