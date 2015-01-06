using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValutaWebForm
{
    public partial class AllValutasForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValutaService.Valuta[] valutas = ValutaServiceContainer.ValutaService.GetValutas();

                foreach (var valuta in valutas)
                {
                    ListBox1.Items.Add(valuta.Iso + " " + valuta.Name + " " + valuta.ExchangeRate);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // BEWARE: selecteditem null
            var selectedItem = ListBox1.SelectedItem.ToString();
            var selectedIso = selectedItem.Split(' ')[0];

            Response.Redirect("SetExchangeRateForm.aspx?iso=" + selectedIso);
        }
    }
}