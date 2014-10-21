using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ValutaWebForm.ValutaService;

namespace ValutaWebForm
{
    public partial class ConversionForm : System.Web.UI.Page
    {
        private ValutaServiceClient valutaService
        {
            get
            {
                return ValutaServiceContainer.ValutaService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Valuta[] valutas = valutaService.GetValutas();

                foreach (Valuta valuta in valutas)
                {
                    FromIsoDropDown.Items.Add(valuta.Iso);
                    ToIsoDropDown.Items.Add(valuta.Iso);
                }
            }
        }

        protected void convertButton_Click(object sender, EventArgs e)
        {
            decimal fromAmount = decimal.Parse(amountTextBox.Text);

            string fromIso = FromIsoDropDown.SelectedItem.ToString();
            string toIso = ToIsoDropDown.SelectedItem.ToString();

            decimal toAmount = valutaService.ConvertFromIsoToIso(fromIso, toIso, fromAmount);

            Label4.Text = toAmount.ToString("N2");
        }
    }
}