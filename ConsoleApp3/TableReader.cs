using System;
using System.Collections.Generic;
using System.Text;

namespace Nelson
{
    public class TableReader
    {
        private readonly WebReader _webReader;

        public TableReader(WebReader webReader)
        {
            _webReader = webReader;
        }
        private string url = "https://www.flashscore.pl/pilka-nozna/anglia/premier-league/tabela/";

        public void ReadTable()
        {
            var urlHtml = _webReader.ReadWebSite(url);
        }
    }
}
