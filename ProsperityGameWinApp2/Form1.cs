using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ProsperityGameWinApp2
{
    public partial class Form1 : Form
    {
        private MongoRepository _mongoRepository;
        public DateTime StartDate;
        public Form1()
        {
            InitializeComponent();
            _mongoRepository = new MongoRepository();
            IntialListView();
            LoadAllListView();
            RefreshCreditValue();
            
            LoadStartDate();
            LoadComboBox();
            
        }

        

        private void LoadStartDate()
        {
            StartDate = _mongoRepository.GetStartDate();
            lblStartDate.Text = ConvertDate(StartDate);
        }

        private int GetDaysFromStart()
        {
            var days = (DateTime.Now.Date - StartDate).TotalDays;
            var castDays = (int)Math.Ceiling(days);
            return castDays == 0 ? 1 : castDays;
        }

        private string ConvertDate(DateTime d)
        {
            var pc = new PersianCalendar();
            return $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)}";
        }

        private void RefreshCreditValue()
        {
            var amount = _mongoRepository.GetTotalCredit();
            lblCashAmount.Text = $@"{amount:n0}";
        }

        private void InsertTask(double money, string description)
        {
            _mongoRepository.Insert(new ProsperityStatus()
            {
                Id = new ObjectId(),
                CreditValue = money * 1000 * GetDaysFromStart(),
                Date = DateTime.Now.Date,
                Description = description,
                DaysFromStart = GetDaysFromStart()
            });
            RefreshCreditValue();
        }

        private void btnAddBuy_Click(object sender, EventArgs e)
        {
            AddBuy form = new AddBuy();
            form.Show(); 
            mainTabControl.SelectedTab = mainTabControl.TabPages[1];
            targetTabControl.SelectedTab = targetTabControl.TabPages[5];
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            AddActivity form = new AddActivity();
            form.Show();
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            var category = (Category)Enum.Parse(typeof(Category), cbGoalCategory.SelectedValue.ToString());
            var goalTitle = txtGoalTitle.Text;
            _mongoRepository.AddGoal(goalTitle, category );
        }

        private void btnShowGoal_Click(object sender, EventArgs e)
        {
            LoadGoalListView();
        }
               
        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            var targetTitle = txtTargetTitle.Text;
            var selectedGoal = (Goal)cbSelectGoal.SelectedItem;
            _mongoRepository.AddTarget(selectedGoal, targetTitle);
        }

        private void btnShowTarget_Click(object sender, EventArgs e)
        {
            LoadTargetListView();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            var priority = (Priority)Enum.Parse(typeof(Priority), cbTaskPeriority.SelectedValue.ToString());
            var taskTitle = txtTaskTitle.Text;
            var selectedTarget = (Target)cbSelectTarget.SelectedItem;
            _mongoRepository.AddTask(selectedTarget, taskTitle, priority);
        }

        private void btnShowTask_Click(object sender, EventArgs e)
        {
            LoadTaskListView();
        }

        private void btnShowBuy_Click(object sender, EventArgs e)
        {
            LoadBuyListView();
        }

        private void btnShowActivity_Click(object sender, EventArgs e)
        {
            LoadActivityListView();
        }

        private void btnAddThank_Click(object sender, EventArgs e)
        {
             
        }

        private void btnShowThank_Click(object sender, EventArgs e)
        {
            LoadThankListView();
        }

        private void LoadComboBox()
        {
            cbGoalCategory.DataSource = Enum.GetValues(typeof(Category));
            cbTaskPeriority.DataSource = Enum.GetValues(typeof(Priority));

            var goalList = _mongoRepository.GetGoalsList().OrderByDescending(i => i.Date);
            cbSelectGoal.DataSource = goalList.ToList();
            cbSelectGoal.DisplayMember = "title";

            var targetList = _mongoRepository.GetTargetsList().OrderByDescending(i => i.Date);
            cbSelectTarget.DataSource = targetList.ToList();
            cbSelectTarget.DisplayMember = "title";

        }

        private void LoadAllListView()
        {
            LoadBuyReport();
            LoadProgressListView();
            LoadGoalListView();
            LoadTargetListView();
            LoadTaskListView();
            LoadActivityListView();
            LoadThankListView();
        }

        private void LoadBuyListView()
        {
            throw new NotImplementedException();
        }

        private void LoadGoalListView()
        {
            financialGoalListView.Items.Clear();
            financialGoalListView.Items.AddRange(GetGoalListView(Category.Financial));

            attractiveGoalListView.Items.Clear();
            attractiveGoalListView.Items.AddRange(GetGoalListView(Category.Attractive));

            BodyGoalListView.Items.Clear();
            BodyGoalListView.Items.AddRange(GetGoalListView(Category.Body));

            CareerGoalListView.Items.Clear();
            CareerGoalListView.Items.AddRange(GetGoalListView(Category.Career));

            educationalGoalListView.Items.Clear();
            educationalGoalListView.Items.AddRange(GetGoalListView(Category.Education));

            healthGoalListView.Items.Clear();
            healthGoalListView.Items.AddRange(GetGoalListView(Category.Health));

            personalityGoalListView.Items.Clear();
            personalityGoalListView.Items.AddRange(GetGoalListView(Category.Personality));

            relationalGoalListView.Items.Clear();
            relationalGoalListView.Items.AddRange(GetGoalListView(Category.Relational));

            skillGoalListView.Items.Clear();
            skillGoalListView.Items.AddRange(GetGoalListView(Category.Skill));

            SpiritualGoalListView.Items.Clear();
            SpiritualGoalListView.Items.AddRange(GetGoalListView(Category.Spiritual));

        }

        private ListViewItem[] GetGoalListView(Category category)
        {
            var result = new List<ListViewItem>();
            var list = _mongoRepository.GetGoalsList().OrderByDescending(i => i.Date).Where(i => i.Category == category);

            foreach (var item in list)
            {
                var record = new ListViewItem(item.Id.ToString());
                record.SubItems.Add(item.Title);
                record.SubItems.Add(ConvertDate(item.Date));

                result.Add(record);
            }
            return result.ToArray();
        }

        private void LoadTargetListView()
        {
            financialTargetListView.Items.Clear();
            financialTargetListView.Items.AddRange(GetTargetListView(Category.Financial));

            attractiveTargetListView.Items.Clear();
            attractiveTargetListView.Items.AddRange(GetTargetListView(Category.Attractive));

            bodyTargetListView.Items.Clear();
            bodyTargetListView.Items.AddRange(GetTargetListView(Category.Body));

            careerTargetListView.Items.Clear();
            careerTargetListView.Items.AddRange(GetTargetListView(Category.Career));

            educationalTargetListView.Items.Clear();
            educationalTargetListView.Items.AddRange(GetTargetListView(Category.Education));

            healthTargetListView.Items.Clear();
            healthTargetListView.Items.AddRange(GetTargetListView(Category.Health));

            personalityTargetListView.Items.Clear();
            personalityTargetListView.Items.AddRange(GetTargetListView(Category.Personality));

            relationalTargetListView.Items.Clear();
            relationalTargetListView.Items.AddRange(GetTargetListView(Category.Relational));

            skillTargetListView.Items.Clear();
            skillTargetListView.Items.AddRange(GetTargetListView(Category.Skill));

            spiritualTargetListView.Items.Clear();
            spiritualTargetListView.Items.AddRange(GetTargetListView(Category.Spiritual));
        }

        private ListViewItem[] GetTargetListView(Category category)
        {
            var result = new List<ListViewItem>();
            var list = _mongoRepository.GetTargetsList().OrderByDescending(i => i.Date).Where(i => i.Category == category);

            foreach (var item in list)
            {
                var record = new ListViewItem(item.Id.ToString());
                record.SubItems.Add(item.Title);
                record.SubItems.Add(ConvertDate(item.Date));

                result.Add(record);
            }
            return result.ToArray();
        }

        private void LoadTaskListView()
        {
            financialTaskListView.Items.Clear();
            financialTaskListView.Items.AddRange(GetTaskListView(Category.Financial));

            attractiveTaskListView.Items.Clear();
            attractiveTaskListView.Items.AddRange(GetTaskListView(Category.Attractive));

            bodyTaskListView.Items.Clear();
            bodyTaskListView.Items.AddRange(GetTaskListView(Category.Body));

            careerTaskListView.Items.Clear();
            careerTaskListView.Items.AddRange(GetTaskListView(Category.Career));

            educationalTaskListView.Items.Clear();
            educationalTaskListView.Items.AddRange(GetTaskListView(Category.Education));

            healthTaskListView.Items.Clear();
            healthTaskListView.Items.AddRange(GetTaskListView(Category.Health));

            personalityTaskListView.Items.Clear();
            personalityTaskListView.Items.AddRange(GetTaskListView(Category.Personality));

            relationalTaskListView.Items.Clear();
            relationalTaskListView.Items.AddRange(GetTaskListView(Category.Relational));

            skillTaskListView.Items.Clear();
            skillTaskListView.Items.AddRange(GetTaskListView(Category.Skill));

            spiritualTaskListView.Items.Clear();
            spiritualTaskListView.Items.AddRange(GetTaskListView(Category.Spiritual));
        }

        private ListViewItem[] GetTaskListView(Category category)
        {
            var result = new List<ListViewItem>();
            var list = _mongoRepository.GetUserTasksList().OrderByDescending(i => i.Date).Where(i => i.Category == category);

            foreach (var item in list)
            {
                var record = new ListViewItem(item.Id.ToString());
                record.SubItems.Add(item.Title);
                record.SubItems.Add(ConvertDate(item.Date));

                result.Add(record);
            }
            return result.ToArray();
        }

        private void LoadProgressListView()
        {
            financialProgressListView.Items.Clear();
            financialProgressListView.Items.AddRange(GetProgressListView(Category.Financial));

            attractiveProgressListView.Items.Clear();
            attractiveProgressListView.Items.AddRange(GetProgressListView(Category.Attractive));

            bodyProgressListView.Items.Clear();
            bodyProgressListView.Items.AddRange(GetProgressListView(Category.Body));

            careerProgressListView.Items.Clear();
            careerProgressListView.Items.AddRange(GetProgressListView(Category.Career));

            educationalProgressListView.Items.Clear();
            educationalProgressListView.Items.AddRange(GetProgressListView(Category.Education));

            healthProgressListView.Items.Clear();
            healthProgressListView.Items.AddRange(GetProgressListView(Category.Health));

            personalityProgressListView.Items.Clear();
            personalityProgressListView.Items.AddRange(GetProgressListView(Category.Personality));

            relationalProgressListView.Items.Clear();
            relationalProgressListView.Items.AddRange(GetProgressListView(Category.Relational));

            skillProgressListView.Items.Clear();
            skillProgressListView.Items.AddRange(GetProgressListView(Category.Skill));

            spiritualProgressListView.Items.Clear();
            spiritualProgressListView.Items.AddRange(GetProgressListView(Category.Spiritual));
        }

        private ListViewItem[] GetProgressListView(Category category)
        {
            var result = new List<ListViewItem>();
            var list = _mongoRepository.GetGoalsList().OrderByDescending(i => i.Date).Where(i => i.Category == category);

            foreach (var item in list)
            {
                var record = new ListViewItem(item.Id.ToString());
                record.SubItems.Add(item.Title);
                record.SubItems.Add(ConvertDate(item.Date));

                result.Add(record);
            }
            return result.ToArray();
        }

        private void LoadActivityListView()
        {
            
        }
        private void LoadThankListView()
        {
            
        }

        private void LoadBuyReport()
        {

            buyListView.Items.Clear();

            var list = _mongoRepository.GetIncomeList().OrderByDescending(i => i.Date).Where(i => i.CreditValue < 0);

            foreach (var item in list)
            {
                var record = new ListViewItem(item.Id.ToString());
                record.SubItems.Add(item.Description);
                record.SubItems.Add(ConvertDate(item.Date));
                record.SubItems.Add($@"{item.CreditValue:n0}");
                record.SubItems.Add(item.DaysFromStart.ToString());

                buyListView.Items.Add(record);
            }

        }

        private void IntialListView()
        {
            IntialGoalListView();
            IntialTargetListView();
            IntialTaskListView();
            IntialThankListView();
            IntialProgressListView();
            IntialBuyListView();
            IntialActivityListView();
            
        }

        private void IntialActivityListView()
        {
            activityListView.View = View.Details;
            activityListView.RightToLeft = RightToLeft.Yes;
            activityListView.RightToLeftLayout = true;
            activityListView.Columns.Add("آیدی", 100);
            activityListView.Columns.Add("توضیحات", 600);
            activityListView.Columns.Add("تاریخ", 120);
            activityListView.Columns.Add("مبلغ", 200);
            activityListView.Columns.Add("روز", 80);
        }

        private void IntialBuyListView()
        {
            buyListView.View = View.Details;
            buyListView.RightToLeft = RightToLeft.Yes;
            buyListView.RightToLeftLayout = true;
            buyListView.Columns.Add("آیدی", 100);
            buyListView.Columns.Add("توضیحات", 600);
            buyListView.Columns.Add("تاریخ", 120);
            buyListView.Columns.Add("مبلغ", 200);
            buyListView.Columns.Add("روز", 80);
        }

        private void IntialGoalListView()
        {
            financialGoalListView.View = View.Details;
            financialGoalListView.RightToLeft = RightToLeft.Yes;
            financialGoalListView.RightToLeftLayout = true;
            financialGoalListView.Columns.Add("آیدی", 100);
            financialGoalListView.Columns.Add("توضیحات", 600);
            financialGoalListView.Columns.Add("تاریخ", 120);
            financialGoalListView.Columns.Add("مبلغ", 200);
            financialGoalListView.Columns.Add("روز", 80);

            attractiveGoalListView.View = View.Details;
            attractiveGoalListView.RightToLeft = RightToLeft.Yes;
            attractiveGoalListView.RightToLeftLayout = true;
            attractiveGoalListView.Columns.Add("آیدی", 100);
            attractiveGoalListView.Columns.Add("توضیحات", 600);
            attractiveGoalListView.Columns.Add("تاریخ", 120);
            attractiveGoalListView.Columns.Add("مبلغ", 200);
            attractiveGoalListView.Columns.Add("روز", 80);

            BodyGoalListView.View = View.Details;
            BodyGoalListView.RightToLeft = RightToLeft.Yes;
            BodyGoalListView.RightToLeftLayout = true;
            BodyGoalListView.Columns.Add("آیدی", 100);
            BodyGoalListView.Columns.Add("توضیحات", 600);
            BodyGoalListView.Columns.Add("تاریخ", 120);
            BodyGoalListView.Columns.Add("مبلغ", 200);
            BodyGoalListView.Columns.Add("روز", 80);

            CareerGoalListView.View = View.Details;
            CareerGoalListView.RightToLeft = RightToLeft.Yes;
            CareerGoalListView.RightToLeftLayout = true;
            CareerGoalListView.Columns.Add("آیدی", 100);
            CareerGoalListView.Columns.Add("توضیحات", 600);
            CareerGoalListView.Columns.Add("تاریخ", 120);
            CareerGoalListView.Columns.Add("مبلغ", 200);
            CareerGoalListView.Columns.Add("روز", 80);

            educationalGoalListView.View = View.Details;
            educationalGoalListView.RightToLeft = RightToLeft.Yes;
            educationalGoalListView.RightToLeftLayout = true;
            educationalGoalListView.Columns.Add("آیدی", 100);
            educationalGoalListView.Columns.Add("توضیحات", 600);
            educationalGoalListView.Columns.Add("تاریخ", 120);
            educationalGoalListView.Columns.Add("مبلغ", 200);
            educationalGoalListView.Columns.Add("روز", 80);

            healthGoalListView.View = View.Details;
            healthGoalListView.RightToLeft = RightToLeft.Yes;
            healthGoalListView.RightToLeftLayout = true;
            healthGoalListView.Columns.Add("آیدی", 100);
            healthGoalListView.Columns.Add("توضیحات", 600);
            healthGoalListView.Columns.Add("تاریخ", 120);
            healthGoalListView.Columns.Add("مبلغ", 200);
            healthGoalListView.Columns.Add("روز", 80);

            personalityGoalListView.View = View.Details;
            personalityGoalListView.RightToLeft = RightToLeft.Yes;
            personalityGoalListView.RightToLeftLayout = true;
            personalityGoalListView.Columns.Add("آیدی", 100);
            personalityGoalListView.Columns.Add("توضیحات", 600);
            personalityGoalListView.Columns.Add("تاریخ", 120);
            personalityGoalListView.Columns.Add("مبلغ", 200);
            personalityGoalListView.Columns.Add("روز", 80);

            relationalGoalListView.View = View.Details;
            relationalGoalListView.RightToLeft = RightToLeft.Yes;
            relationalGoalListView.RightToLeftLayout = true;
            relationalGoalListView.Columns.Add("آیدی", 100);
            relationalGoalListView.Columns.Add("توضیحات", 600);
            relationalGoalListView.Columns.Add("تاریخ", 120);
            relationalGoalListView.Columns.Add("مبلغ", 200);
            relationalGoalListView.Columns.Add("روز", 80);

            skillGoalListView.View = View.Details;
            skillGoalListView.RightToLeft = RightToLeft.Yes;
            skillGoalListView.RightToLeftLayout = true;
            skillGoalListView.Columns.Add("آیدی", 100);
            skillGoalListView.Columns.Add("توضیحات", 600);
            skillGoalListView.Columns.Add("تاریخ", 120);
            skillGoalListView.Columns.Add("مبلغ", 200);
            skillGoalListView.Columns.Add("روز", 80);

            SpiritualGoalListView.View = View.Details;
            SpiritualGoalListView.RightToLeft = RightToLeft.Yes;
            SpiritualGoalListView.RightToLeftLayout = true;
            SpiritualGoalListView.Columns.Add("آیدی", 100);
            SpiritualGoalListView.Columns.Add("توضیحات", 600);
            SpiritualGoalListView.Columns.Add("تاریخ", 120);
            SpiritualGoalListView.Columns.Add("مبلغ", 200);
            SpiritualGoalListView.Columns.Add("روز", 80);
        }

        private void IntialTargetListView()
        {
            financialTargetListView.View = View.Details;
            financialTargetListView.RightToLeft = RightToLeft.Yes;
            financialTargetListView.RightToLeftLayout = true;
            financialTargetListView.Columns.Add("آیدی", 100);
            financialTargetListView.Columns.Add("توضیحات", 600);
            financialTargetListView.Columns.Add("تاریخ", 120);
            financialTargetListView.Columns.Add("مبلغ", 200);
            financialTargetListView.Columns.Add("روز", 80);

            attractiveTargetListView.View = View.Details;
            attractiveTargetListView.RightToLeft = RightToLeft.Yes;
            attractiveTargetListView.RightToLeftLayout = true;
            attractiveTargetListView.Columns.Add("آیدی", 100);
            attractiveTargetListView.Columns.Add("توضیحات", 600);
            attractiveTargetListView.Columns.Add("تاریخ", 120);
            attractiveTargetListView.Columns.Add("مبلغ", 200);
            attractiveTargetListView.Columns.Add("روز", 80);

            bodyTargetListView.View = View.Details;
            bodyTargetListView.RightToLeft = RightToLeft.Yes;
            bodyTargetListView.RightToLeftLayout = true;
            bodyTargetListView.Columns.Add("آیدی", 100);
            bodyTargetListView.Columns.Add("توضیحات", 600);
            bodyTargetListView.Columns.Add("تاریخ", 120);
            bodyTargetListView.Columns.Add("مبلغ", 200);
            bodyTargetListView.Columns.Add("روز", 80);

            careerTargetListView.View = View.Details;
            careerTargetListView.RightToLeft = RightToLeft.Yes;
            careerTargetListView.RightToLeftLayout = true;
            careerTargetListView.Columns.Add("آیدی", 100);
            careerTargetListView.Columns.Add("توضیحات", 600);
            careerTargetListView.Columns.Add("تاریخ", 120);
            careerTargetListView.Columns.Add("مبلغ", 200);
            careerTargetListView.Columns.Add("روز", 80);

            educationalTargetListView.View = View.Details;
            educationalTargetListView.RightToLeft = RightToLeft.Yes;
            educationalTargetListView.RightToLeftLayout = true;
            educationalTargetListView.Columns.Add("آیدی", 100);
            educationalTargetListView.Columns.Add("توضیحات", 600);
            educationalTargetListView.Columns.Add("تاریخ", 120);
            educationalTargetListView.Columns.Add("مبلغ", 200);
            educationalTargetListView.Columns.Add("روز", 80);

            healthTargetListView.View = View.Details;
            healthTargetListView.RightToLeft = RightToLeft.Yes;
            healthTargetListView.RightToLeftLayout = true;
            healthTargetListView.Columns.Add("آیدی", 100);
            healthTargetListView.Columns.Add("توضیحات", 600);
            healthTargetListView.Columns.Add("تاریخ", 120);
            healthTargetListView.Columns.Add("مبلغ", 200);
            healthTargetListView.Columns.Add("روز", 80);

            personalityTargetListView.View = View.Details;
            personalityTargetListView.RightToLeft = RightToLeft.Yes;
            personalityTargetListView.RightToLeftLayout = true;
            personalityTargetListView.Columns.Add("آیدی", 100);
            personalityTargetListView.Columns.Add("توضیحات", 600);
            personalityTargetListView.Columns.Add("تاریخ", 120);
            personalityTargetListView.Columns.Add("مبلغ", 200);
            personalityTargetListView.Columns.Add("روز", 80);

            relationalTargetListView.View = View.Details;
            relationalTargetListView.RightToLeft = RightToLeft.Yes;
            relationalTargetListView.RightToLeftLayout = true;
            relationalTargetListView.Columns.Add("آیدی", 100);
            relationalTargetListView.Columns.Add("توضیحات", 600);
            relationalTargetListView.Columns.Add("تاریخ", 120);
            relationalTargetListView.Columns.Add("مبلغ", 200);
            relationalTargetListView.Columns.Add("روز", 80);

            skillTargetListView.View = View.Details;
            skillTargetListView.RightToLeft = RightToLeft.Yes;
            skillTargetListView.RightToLeftLayout = true;
            skillTargetListView.Columns.Add("آیدی", 100);
            skillTargetListView.Columns.Add("توضیحات", 600);
            skillTargetListView.Columns.Add("تاریخ", 120);
            skillTargetListView.Columns.Add("مبلغ", 200);
            skillTargetListView.Columns.Add("روز", 80);

            spiritualTargetListView.View = View.Details;
            spiritualTargetListView.RightToLeft = RightToLeft.Yes;
            spiritualTargetListView.RightToLeftLayout = true;
            spiritualTargetListView.Columns.Add("آیدی", 100);
            spiritualTargetListView.Columns.Add("توضیحات", 600);
            spiritualTargetListView.Columns.Add("تاریخ", 120);
            spiritualTargetListView.Columns.Add("مبلغ", 200);
            spiritualTargetListView.Columns.Add("روز", 80);
        }

        private void IntialTaskListView()
        {
            financialTaskListView.View = View.Details;
            financialTaskListView.RightToLeft = RightToLeft.Yes;
            financialTaskListView.RightToLeftLayout = true;
            financialTaskListView.Columns.Add("آیدی", 100);
            financialTaskListView.Columns.Add("توضیحات", 600);
            financialTaskListView.Columns.Add("تاریخ", 120);
            financialTaskListView.Columns.Add("مبلغ", 200);
            financialTaskListView.Columns.Add("روز", 80);

            attractiveTaskListView.View = View.Details;
            attractiveTaskListView.RightToLeft = RightToLeft.Yes;
            attractiveTaskListView.RightToLeftLayout = true;
            attractiveTaskListView.Columns.Add("آیدی", 100);
            attractiveTaskListView.Columns.Add("توضیحات", 600);
            attractiveTaskListView.Columns.Add("تاریخ", 120);
            attractiveTaskListView.Columns.Add("مبلغ", 200);
            attractiveTaskListView.Columns.Add("روز", 80);

            bodyTaskListView.View = View.Details;
            bodyTaskListView.RightToLeft = RightToLeft.Yes;
            bodyTaskListView.RightToLeftLayout = true;
            bodyTaskListView.Columns.Add("آیدی", 100);
            bodyTaskListView.Columns.Add("توضیحات", 600);
            bodyTaskListView.Columns.Add("تاریخ", 120);
            bodyTaskListView.Columns.Add("مبلغ", 200);
            bodyTaskListView.Columns.Add("روز", 80);

            careerTaskListView.View = View.Details;
            careerTaskListView.RightToLeft = RightToLeft.Yes;
            careerTaskListView.RightToLeftLayout = true;
            careerTaskListView.Columns.Add("آیدی", 100);
            careerTaskListView.Columns.Add("توضیحات", 600);
            careerTaskListView.Columns.Add("تاریخ", 120);
            careerTaskListView.Columns.Add("مبلغ", 200);
            careerTaskListView.Columns.Add("روز", 80);

            educationalTaskListView.View = View.Details;
            educationalTaskListView.RightToLeft = RightToLeft.Yes;
            educationalTaskListView.RightToLeftLayout = true;
            educationalTaskListView.Columns.Add("آیدی", 100);
            educationalTaskListView.Columns.Add("توضیحات", 600);
            educationalTaskListView.Columns.Add("تاریخ", 120);
            educationalTaskListView.Columns.Add("مبلغ", 200);
            educationalTaskListView.Columns.Add("روز", 80);

            healthTaskListView.View = View.Details;
            healthTaskListView.RightToLeft = RightToLeft.Yes;
            healthTaskListView.RightToLeftLayout = true;
            healthTaskListView.Columns.Add("آیدی", 100);
            healthTaskListView.Columns.Add("توضیحات", 600);
            healthTaskListView.Columns.Add("تاریخ", 120);
            healthTaskListView.Columns.Add("مبلغ", 200);
            healthTaskListView.Columns.Add("روز", 80);

            personalityTaskListView.View = View.Details;
            personalityTaskListView.RightToLeft = RightToLeft.Yes;
            personalityTaskListView.RightToLeftLayout = true;
            personalityTaskListView.Columns.Add("آیدی", 100);
            personalityTaskListView.Columns.Add("توضیحات", 600);
            personalityTaskListView.Columns.Add("تاریخ", 120);
            personalityTaskListView.Columns.Add("مبلغ", 200);
            personalityTaskListView.Columns.Add("روز", 80);

            relationalTaskListView.View = View.Details;
            relationalTaskListView.RightToLeft = RightToLeft.Yes;
            relationalTaskListView.RightToLeftLayout = true;
            relationalTaskListView.Columns.Add("آیدی", 100);
            relationalTaskListView.Columns.Add("توضیحات", 600);
            relationalTaskListView.Columns.Add("تاریخ", 120);
            relationalTaskListView.Columns.Add("مبلغ", 200);
            relationalTaskListView.Columns.Add("روز", 80);

            skillTaskListView.View = View.Details;
            skillTaskListView.RightToLeft = RightToLeft.Yes;
            skillTaskListView.RightToLeftLayout = true;
            skillTaskListView.Columns.Add("آیدی", 100);
            skillTaskListView.Columns.Add("توضیحات", 600);
            skillTaskListView.Columns.Add("تاریخ", 120);
            skillTaskListView.Columns.Add("مبلغ", 200);
            skillTaskListView.Columns.Add("روز", 80);

            spiritualTaskListView.View = View.Details;
            spiritualTaskListView.RightToLeft = RightToLeft.Yes;
            spiritualTaskListView.RightToLeftLayout = true;
            spiritualTaskListView.Columns.Add("آیدی", 100);
            spiritualTaskListView.Columns.Add("توضیحات", 600);
            spiritualTaskListView.Columns.Add("تاریخ", 120);
            spiritualTaskListView.Columns.Add("مبلغ", 200);
            spiritualTaskListView.Columns.Add("روز", 80);
        }

        private void IntialThankListView()
        {
            financialThankListView.View = View.Details;
            financialThankListView.RightToLeft = RightToLeft.Yes;
            financialThankListView.RightToLeftLayout = true;
            financialThankListView.Columns.Add("آیدی", 100);
            financialThankListView.Columns.Add("توضیحات", 600);
            financialThankListView.Columns.Add("تاریخ", 120);
            financialThankListView.Columns.Add("مبلغ", 200);
            financialThankListView.Columns.Add("روز", 80);

            attractiveThankListView.View = View.Details;
            attractiveThankListView.RightToLeft = RightToLeft.Yes;
            attractiveThankListView.RightToLeftLayout = true;
            attractiveThankListView.Columns.Add("آیدی", 100);
            attractiveThankListView.Columns.Add("توضیحات", 600);
            attractiveThankListView.Columns.Add("تاریخ", 120);
            attractiveThankListView.Columns.Add("مبلغ", 200);
            attractiveThankListView.Columns.Add("روز", 80);

            bodyThankListView.View = View.Details;
            bodyThankListView.RightToLeft = RightToLeft.Yes;
            bodyThankListView.RightToLeftLayout = true;
            bodyThankListView.Columns.Add("آیدی", 100);
            bodyThankListView.Columns.Add("توضیحات", 600);
            bodyThankListView.Columns.Add("تاریخ", 120);
            bodyThankListView.Columns.Add("مبلغ", 200);
            bodyThankListView.Columns.Add("روز", 80);

            careerThankListView.View = View.Details;
            careerThankListView.RightToLeft = RightToLeft.Yes;
            careerThankListView.RightToLeftLayout = true;
            careerThankListView.Columns.Add("آیدی", 100);
            careerThankListView.Columns.Add("توضیحات", 600);
            careerThankListView.Columns.Add("تاریخ", 120);
            careerThankListView.Columns.Add("مبلغ", 200);
            careerThankListView.Columns.Add("روز", 80);

            educationalThankListView.View = View.Details;
            educationalThankListView.RightToLeft = RightToLeft.Yes;
            educationalThankListView.RightToLeftLayout = true;
            educationalThankListView.Columns.Add("آیدی", 100);
            educationalThankListView.Columns.Add("توضیحات", 600);
            educationalThankListView.Columns.Add("تاریخ", 120);
            educationalThankListView.Columns.Add("مبلغ", 200);
            educationalThankListView.Columns.Add("روز", 80);

            healthThankListView.View = View.Details;
            healthThankListView.RightToLeft = RightToLeft.Yes;
            healthThankListView.RightToLeftLayout = true;
            healthThankListView.Columns.Add("آیدی", 100);
            healthThankListView.Columns.Add("توضیحات", 600);
            healthThankListView.Columns.Add("تاریخ", 120);
            healthThankListView.Columns.Add("مبلغ", 200);
            healthThankListView.Columns.Add("روز", 80);

            personalityThankListView.View = View.Details;
            personalityThankListView.RightToLeft = RightToLeft.Yes;
            personalityThankListView.RightToLeftLayout = true;
            personalityThankListView.Columns.Add("آیدی", 100);
            personalityThankListView.Columns.Add("توضیحات", 600);
            personalityThankListView.Columns.Add("تاریخ", 120);
            personalityThankListView.Columns.Add("مبلغ", 200);
            personalityThankListView.Columns.Add("روز", 80);

            relationalThankListView.View = View.Details;
            relationalThankListView.RightToLeft = RightToLeft.Yes;
            relationalThankListView.RightToLeftLayout = true;
            relationalThankListView.Columns.Add("آیدی", 100);
            relationalThankListView.Columns.Add("توضیحات", 600);
            relationalThankListView.Columns.Add("تاریخ", 120);
            relationalThankListView.Columns.Add("مبلغ", 200);
            relationalThankListView.Columns.Add("روز", 80);

            skillThankListView.View = View.Details;
            skillThankListView.RightToLeft = RightToLeft.Yes;
            skillThankListView.RightToLeftLayout = true;
            skillThankListView.Columns.Add("آیدی", 100);
            skillThankListView.Columns.Add("توضیحات", 600);
            skillThankListView.Columns.Add("تاریخ", 120);
            skillThankListView.Columns.Add("مبلغ", 200);
            skillThankListView.Columns.Add("روز", 80);

            spiritualThankListView.View = View.Details;
            spiritualThankListView.RightToLeft = RightToLeft.Yes;
            spiritualThankListView.RightToLeftLayout = true;
            spiritualThankListView.Columns.Add("آیدی", 100);
            spiritualThankListView.Columns.Add("توضیحات", 600);
            spiritualThankListView.Columns.Add("تاریخ", 120);
            spiritualThankListView.Columns.Add("مبلغ", 200);
            spiritualThankListView.Columns.Add("روز", 80);
        }

        private void IntialProgressListView()
        {
            financialProgressListView.View = View.Details;
            financialProgressListView.RightToLeft = RightToLeft.Yes;
            financialProgressListView.RightToLeftLayout = true;
            financialProgressListView.Columns.Add("آیدی", 100);
            financialProgressListView.Columns.Add("توضیحات", 600);
            financialProgressListView.Columns.Add("تاریخ", 120);
            financialProgressListView.Columns.Add("مبلغ", 200);
            financialProgressListView.Columns.Add("روز", 80);

            attractiveProgressListView.View = View.Details;
            attractiveProgressListView.RightToLeft = RightToLeft.Yes;
            attractiveProgressListView.RightToLeftLayout = true;
            attractiveProgressListView.Columns.Add("آیدی", 100);
            attractiveProgressListView.Columns.Add("توضیحات", 600);
            attractiveProgressListView.Columns.Add("تاریخ", 120);
            attractiveProgressListView.Columns.Add("مبلغ", 200);
            attractiveProgressListView.Columns.Add("روز", 80);

            bodyProgressListView.View = View.Details;
            bodyProgressListView.RightToLeft = RightToLeft.Yes;
            bodyProgressListView.RightToLeftLayout = true;
            bodyProgressListView.Columns.Add("آیدی", 100);
            bodyProgressListView.Columns.Add("توضیحات", 600);
            bodyProgressListView.Columns.Add("تاریخ", 120);
            bodyProgressListView.Columns.Add("مبلغ", 200);
            bodyProgressListView.Columns.Add("روز", 80);

            careerProgressListView.View = View.Details;
            careerProgressListView.RightToLeft = RightToLeft.Yes;
            careerProgressListView.RightToLeftLayout = true;
            careerProgressListView.Columns.Add("آیدی", 100);
            careerProgressListView.Columns.Add("توضیحات", 600);
            careerProgressListView.Columns.Add("تاریخ", 120);
            careerProgressListView.Columns.Add("مبلغ", 200);
            careerProgressListView.Columns.Add("روز", 80);

            educationalProgressListView.View = View.Details;
            educationalProgressListView.RightToLeft = RightToLeft.Yes;
            educationalProgressListView.RightToLeftLayout = true;
            educationalProgressListView.Columns.Add("آیدی", 100);
            educationalProgressListView.Columns.Add("توضیحات", 600);
            educationalProgressListView.Columns.Add("تاریخ", 120);
            educationalProgressListView.Columns.Add("مبلغ", 200);
            educationalProgressListView.Columns.Add("روز", 80);

            healthProgressListView.View = View.Details;
            healthProgressListView.RightToLeft = RightToLeft.Yes;
            healthProgressListView.RightToLeftLayout = true;
            healthProgressListView.Columns.Add("آیدی", 100);
            healthProgressListView.Columns.Add("توضیحات", 600);
            healthProgressListView.Columns.Add("تاریخ", 120);
            healthProgressListView.Columns.Add("مبلغ", 200);
            healthProgressListView.Columns.Add("روز", 80);

            personalityProgressListView.View = View.Details;
            personalityProgressListView.RightToLeft = RightToLeft.Yes;
            personalityProgressListView.RightToLeftLayout = true;
            personalityProgressListView.Columns.Add("آیدی", 100);
            personalityProgressListView.Columns.Add("توضیحات", 600);
            personalityProgressListView.Columns.Add("تاریخ", 120);
            personalityProgressListView.Columns.Add("مبلغ", 200);
            personalityProgressListView.Columns.Add("روز", 80);

            relationalProgressListView.View = View.Details;
            relationalProgressListView.RightToLeft = RightToLeft.Yes;
            relationalProgressListView.RightToLeftLayout = true;
            relationalProgressListView.Columns.Add("آیدی", 100);
            relationalProgressListView.Columns.Add("توضیحات", 600);
            relationalProgressListView.Columns.Add("تاریخ", 120);
            relationalProgressListView.Columns.Add("مبلغ", 200);
            relationalProgressListView.Columns.Add("روز", 80);

            skillProgressListView.View = View.Details;
            skillProgressListView.RightToLeft = RightToLeft.Yes;
            skillProgressListView.RightToLeftLayout = true;
            skillProgressListView.Columns.Add("آیدی", 100);
            skillProgressListView.Columns.Add("توضیحات", 600);
            skillProgressListView.Columns.Add("تاریخ", 120);
            skillProgressListView.Columns.Add("مبلغ", 200);
            skillProgressListView.Columns.Add("روز", 80);

            spiritualProgressListView.View = View.Details;
            spiritualProgressListView.RightToLeft = RightToLeft.Yes;
            spiritualProgressListView.RightToLeftLayout = true;
            spiritualProgressListView.Columns.Add("آیدی", 100);
            spiritualProgressListView.Columns.Add("توضیحات", 600);
            spiritualProgressListView.Columns.Add("تاریخ", 120);
            spiritualProgressListView.Columns.Add("مبلغ", 200);
            spiritualProgressListView.Columns.Add("روز", 80);
        }

    }
}
