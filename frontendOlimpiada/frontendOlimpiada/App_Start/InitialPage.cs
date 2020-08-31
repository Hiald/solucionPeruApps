using System;
using System.Web.Mvc;

namespace frontendOlimpiada.App_Start
{
    public abstract class InitialPage<T> : WebViewPage<T>
    {
        protected override void InitializePage()
        {
            SetViewBagDefaultProperties();
            base.InitializePage();
        }
        private void SetViewBagDefaultProperties()
        {
            ViewBag.sFecha = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}