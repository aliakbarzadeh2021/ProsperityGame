using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProsperityGameWinApp2
{
    public partial class AddBuy : Form
    {
        private MongoRepository _mongoRepository;
        public DateTime StartDate;

        public AddBuy(MongoRepository mongoRepository, DateTime startDate)
        {
            InitializeComponent();
            StartDate = startDate;
            _mongoRepository = mongoRepository;
        }

        public AddBuy()
        {
            InitializeComponent();
        }

        private void btnAddBuy_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var price = txtPrice.Text;
            _mongoRepository.AddBuy(StartDate, title, price);
        }
    }
}
