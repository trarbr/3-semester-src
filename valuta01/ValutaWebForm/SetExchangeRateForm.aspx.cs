using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ValutaWebForm.ValutaService;

namespace ValutaWebForm
{
    public partial class FindValutaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Valuta[] valutas = ValutaServiceContainer.ValutaService.GetValutas();
                foreach (var valuta in valutas)
                {
                    valutaDropDown.Items.Add(valuta.Iso);
                }
            }
        }

        protected void setExchangeRateButton_Click(object sender, EventArgs e)
        {
            decimal exchangeRate = decimal.Parse(exchangeRateTextBox.Text);

            Valuta selectedValuta = findValuta(valutaDropDown.SelectedItem.ToString());

            selectedValuta.ExchangeRate = exchangeRate;

            bool success = ValutaServiceContainer.ValutaService.SetValutaExchangeRate(selectedValuta);
            if (success)
            {
                successLabel.Text = "Exchange Rate updated.";
            }
            else
            {
                successLabel.Text = "Setting the exchange rate failed.";
            }
        }

        private Valuta findValuta(string iso)
        {
            Valuta[] valutas = ValutaServiceContainer.ValutaService.GetValutas();

            return valutas.Where(v => v.Iso == iso).FirstOrDefault();
        }
    }
}