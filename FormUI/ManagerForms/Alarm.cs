﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FormUI.OperationLayer;

namespace FormUI.ManagerForms
{
    public partial class Alarm : Form
    {
        private IList<ListViewItem> Items;
        private OrderDefinition _order;
        public Alarm( IList<ListViewItem> items)
        {
            InitializeComponent();
            Items = items;
            _order = new OrderDefinition();
            txtＭinute.KeyPress += Infrastructure.Handler.Nuber09;

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(@"确定发送至选中的{0}个终端？", Items.Count),
                                "提示",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    var t = new ThreadStart(() =>
                        {
                            foreach (var item in Items)
                            {
                                _order.Alarm(item.Text, item.ToolTipText, txtＭinute.Text.Trim());
                            }
                        });
                    new Thread(t).Start();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
                this.Close();
            }
        }

     

       
    }
}
