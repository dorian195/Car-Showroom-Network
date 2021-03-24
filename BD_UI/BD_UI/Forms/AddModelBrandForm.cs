﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BD_UI.Database;
using BD_UI.Database.Domain;

namespace BD_UI
{
    public partial class AddModelBrandForm : Form
    {
        private DatabaseContext databaseContext;

        public AddModelBrandForm()
        {
            DesignTimeDbContextFactory factory = new DesignTimeDbContextFactory();
            databaseContext = factory.CreateDbContext();
            InitializeComponent();
            fillComboBox();
        }

        private void fillComboBox()
        {
            var brands = databaseContext.Set<CarBrands>();
            foreach (CarBrands carBrand in brands)
            {
                comboBoxBrand.Items.Add(carBrand.Name);
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxBrandName.Text))
            {
                var brand = databaseContext.Set<CarBrands>();
                brand.Add(new CarBrands
                {
                    Name = textBoxBrandName.Text
                });
            }
            if (!String.IsNullOrWhiteSpace(textBoxWorkerName.Text))
            {
                var job = databaseContext.Set<Jobs>();
                job.Add(new Jobs
                {
                    Name = textBoxWorkerName.Text
                });
            }
            if (!String.IsNullOrWhiteSpace(textBoxModel.Text))
            {
                var model = databaseContext.Set<Models>();
                model.Add(new Models
                {
                    Name = textBoxModel.Text,
                    Brand = databaseContext.Set<CarBrands>()
                    .Where(brand => brand.Name == comboBoxBrand.Text)
                    .FirstOrDefault<CarBrands>()
                });
            }

            databaseContext.SaveChanges();
            this.Close();
        }
    }
}
