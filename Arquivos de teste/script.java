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
		beginStep("[1] Moodle (/moodle.unipampa.edu.br/)", 0);
		{
			web.window(2, "/web:window[@index='0' or @title='about:blank']")
					.navigate("https://moodle.unipampa.edu.br/");
			web.window(4, "/web:window[@index='0' or @title='Moodle']")
					.waitForPage(null);
			{
				think(1.277);
			}
			web.link(
					5,
					"/web:window[@index='0' or @title='Moodle']/web:document[@index='0']/web:a[@text='ACESSAR' or @href='https://moodle.unipampa.edu.br/moodle' or @index='0']")
					.click();
		}
		endStep();
		beginStep("[2] UNIPAMPA (/moodle/)", 0);
		{
			web.window(6, "/web:window[@index='0' or @title='UNIPAMPA']")
					.waitForPage(null);
			{
				think(1.563);
			}
			web.link(
					7,
					"/web:window[@index='0' or @title='UNIPAMPA']/web:document[@index='0']/web:a[@text='Acessar' or @href='https://moodle.unipampa.edu.br/moodle/login/index.php' or @index='2']")
					.click();
		}
		endStep();
		beginStep("[3] UNIPAMPA: Acesso ao site (/index.php)", 0);
		{
			web.window(8,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']")
					.waitForPage(null);
			{
				think(1.436);
			}
			web.textBox(
					9,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_text[@id='username' or @name='username' or @index='0']")
					.setText("");
			{
				think(0.02);
			}
			web.textBox(
					10,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_text[@id='username' or @name='username' or @index='0']")
					.click();
			{
				think(2.619);
			}
			web.textBox(
					11,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_text[@id='username' or @name='username' or @index='0']")
					.setText("161150704");
			{
				think(0.034);
			}
			web.textBox(
					12,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_password[@id='password' or @name='password' or @index='0']")
					.setPassword(deobfuscate("QPNM89XmJxH6cIzrFBjvSw=="));
			{
				think(0.009);
			}
			web.textBox(
					13,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_password[@id='password' or @name='password' or @index='0']")
					.click();
			{
				think(2.291);
			}
			web.textBox(
					14,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:input_password[@id='password' or @name='password' or @index='0']")
					.setPassword(deobfuscate("j96SWN9/+XIx9NzHu3x1CA=="));
			{
				think(0.02);
			}
			web.button(
					15,
					"/web:window[@index='0' or @title='UNIPAMPA: Acesso ao site']/web:document[@index='0']/web:form[@id='login' or @index='0']/web:button[@id='loginbtn' or @index='0']")
					.click();
		}
		endStep();
		beginStep("[4] UNIPAMPA (/moodle/)", 0);
		{
			web.window(16, "/web:window[@index='0' or @title='UNIPAMPA']")
					.waitForPage(null);
		}
		endStep();

	}
	
	public void finish() throws Exception {
	}
}
