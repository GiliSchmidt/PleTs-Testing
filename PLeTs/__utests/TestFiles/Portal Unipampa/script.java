import oracle.oats.scripting.modules.basic.api.*;
import oracle.oats.scripting.modules.browser.api.*;
import oracle.oats.scripting.modules.functionalTest.api.*;
import oracle.oats.scripting.modules.utilities.api.*;
import oracle.oats.scripting.modules.utilities.api.sql.*;
import oracle.oats.scripting.modules.utilities.api.xml.*;
import oracle.oats.scripting.modules.utilities.api.file.*;
import oracle.oats.scripting.modules.webdom.api.*;

public class script extends IteratingVUserScript {
	@ScriptService oracle.oats.scripting.modules.utilities.api.UtilitiesService utilities;
	@ScriptService oracle.oats.scripting.modules.browser.api.BrowserService browser;
	@ScriptService oracle.oats.scripting.modules.functionalTest.api.FunctionalTestService ft;
	@ScriptService oracle.oats.scripting.modules.webdom.api.WebDomService web;
	
	public void initialize() throws Exception {
		browser.launch();
	}
		
	/**
	 * Add code to be executed each iteration for this virtual user.
	 */
	public void run() throws Exception {
		beginStep(
				"[1] Unipampa | Universidade Federal do Pampa (/novoportal/)",
				0);
		{
			web.window(31,
					"/web:window[@index='0' or @title='novoportal.unipampa.edu.br/novoportal/']")
					.navigate("http://novoportal.unipampa.edu.br/novoportal/");
			web.window(33,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']")
					.waitForPage(null);
			{
				think(1.203);
			}
			web.textBox(
					34,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']/web:document[@index='0']/web:form[@id='search-block-form' or @index='0']/web:input_text[@id='edit-search-block-form--2' or @name='search_block_form' or @index='0']")
					.click();
			{
				think(1.152);
			}
			web.textBox(
					35,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']/web:document[@index='0']/web:form[@id='search-block-form' or @index='0']/web:input_text[@id='edit-search-block-form--2' or @name='search_block_form' or @index='0']")
					.setText("banana");
			{
				think(4.967);
			}
			web.button(
					36,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']/web:document[@index='0']/web:form[@id='search-block-form' or @index='0']/web:input_submit[@id='edit-submit' or @name='op' or @value='   ' or @index='0']")
					.click();
		}
		endStep();
		beginStep("[2] Buscar | Unipampa (/banana)", 0);
		{
			web.window(37,
					"/web:window[@index='0' or @title='Buscar | Unipampa']")
					.waitForPage(null);
			{
				think(14.543);
			}
			web.image(
					38,
					"/web:window[@index='0' or @title='Buscar | Unipampa']/web:document[@index='0']/web:img[@alt='Unipampa' or @id='logo' or @index='1' or @src='http://novoportal.unipampa.edu.br/novoportal/sites/all/themes/unipampa/logo.png']")
					.click();
		}
		endStep();
		beginStep(
				"[3] Unipampa | Universidade Federal do Pampa (/novoportal/)",
				0);
		{
			web.window(39,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']")
					.waitForPage(null);
			{
				think(6.292);
			}
			web.link(
					40,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']/web:document[@index='0']/web:a[@text='Alegrete' or @href='http://novoportal.unipampa.edu.br/alegrete' or @index='168']")
					.click();
		}
		endStep();
		beginStep("[4] Campus Alegrete - Unipampa (/alegrete/)", 0);
		{
			web.window(41,
					"/web:window[@index='0' or @title='Campus Alegrete - Unipampa']")
					.waitForPage(null);
		}
		endStep();

	}
	
	public void finish() throws Exception {
	}
}
