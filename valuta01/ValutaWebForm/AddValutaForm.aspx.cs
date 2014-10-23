using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValutaWebForm
{
    public partial class AddValutaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addValutaButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string iso = isoTextBox.Text;
            decimal exchangeRate = decimal.Parse(exchangeRateTextBox.Text);

            ValutaService.Valuta valuta = new ValutaService.Valuta()
            {
                Name = name,
                Iso = iso,
                ExchangeRate = exchangeRate
            };

            bool success = ValutaServiceContainer.ValutaService.AddValuta(valuta);

            if (success)
            {
                successLabel.Text = "Valuta successfully created!";
            }
            else
            {
                successLabel.Text = "Error, error! Valuta not created!";
            }
        }
    }
}