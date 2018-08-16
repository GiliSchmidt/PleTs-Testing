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
					.setText("");
			{
				think(4.967);
			}
			web.button(
					36,
					"/web:window[@index='0' or @title='Unipampa | Universidade Federal do Pampa']/web:document[@index='0']/web:form[@id='search-block-form' or @index='0']/web:input_submit[@id='edit-submit' or @name='op' or @value='   ' or @index='0']")
					.click();
		}
		endStep();
	}
	
	public void finish() throws Exception {
	}
}
