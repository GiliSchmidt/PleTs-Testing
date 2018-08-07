Action()
{
web_set_max_html_param_len("9999999");
lr_start_transaction("uc_custom_orderassociation_and_unprocessing");


	lr_think_time(1);


	lr_start_transaction("OOTB_LoginPageLoad");
		lr_start_sub_transaction("LoginPageLoad", "OOTB_LoginPageLoad");
			web_url("LoginPageLoad",
				"URL=http://{server_server}",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("LoginPageLoad", LR_AUTO);


		lr_start_sub_transaction("UpdateBrowserLevel_aspx", "OOTB_LoginPageLoad");
			web_custom_request("UpdateBrowserLevel.aspx",
				"URL=http://{server_server}/core/UpdateBrowserLevel.aspx?Action=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("UpdateBrowserLevel_aspx", LR_AUTO);


		lr_start_sub_transaction("fsLogin_asp", "OOTB_LoginPageLoad");
			web_custom_request("fsLogin.asp",
				"URL=http://{server_server}/fsLogin.asp?msps=0&LangId=E&CodeId=USA",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/",
				"Mode=HTTP",
				"Body=<root><type>NT</type><p1></p1><p2></p2><p3>0</p3><p5></p5><p6></p6><p7></p7><p8>00000000-0000-0000-0000-000000000000</p8></root>",
LAST);
		lr_end_sub_transaction("fsLogin_asp", LR_AUTO);


		lr_start_sub_transaction("UpdateBrowserLevel_aspx_2", "OOTB_LoginPageLoad");
			web_custom_request("UpdateBrowserLevel.aspx_2",
				"URL=http://{server_server}/core/UpdateBrowserLevel.aspx?Action=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("UpdateBrowserLevel_aspx_2", LR_AUTO);


	lr_end_transaction("OOTB_LoginPageLoad",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_LoginSubmit");
		lr_start_sub_transaction("Login Submit", "OOTB_LoginSubmit");
			web_custom_request("Login Submit",
				"URL=http://{server_server}/fsLogin.asp?msps=0&LangId=E&CodeId=USA",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/",
				"Mode=HTTP",
				"Body=<root><type>SQL</type><p1>{cpbusinessadmin_emailaddress}</p1><p2>password</p2><p3>0</p3><p5></p5><p6></p6><p7></p7><p8>00000000-0000-0000-0000-000000000000</p8></root>",
LAST);
		lr_end_sub_transaction("Login Submit", LR_AUTO);


		lr_start_sub_transaction("UpdateBrowserLevel_aspx_3", "OOTB_LoginSubmit");
			web_custom_request("UpdateBrowserLevel.aspx_3",
				"URL=http://{server_server}/core/UpdateBrowserLevel.aspx?Action=1&SessionID={{sno}}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("UpdateBrowserLevel_aspx_3", LR_AUTO);


		lr_start_sub_transaction("uiBrowser_aspx", "OOTB_LoginSubmit");
			web_url("uiBrowser.aspx",
				"URL=http://{server_server}/core/ui/uiBrowser.aspx?rId={{rid}}&ui=W&sno={{sno}}&login=NT&cars=false",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("uiBrowser_aspx", LR_AUTO);


		lr_start_sub_transaction("inQuickLink_aspx", "OOTB_LoginSubmit");
			web_custom_request("inQuickLink_aspx",
				"URL=http://{server_server}/IncludeFiles/inQuickLink.aspx?action=1&rid={{rid}}&sno={{sno}}&ui=W&home=Yes",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/ui/uiBrowser.aspx?rId={{rid}}&ui=W&sno={{sno}}&login=NT&cars=false",
				"Mode=HTTP",
				"Body=<root><url1>/CORE/UI/HOME.ASPX</url1></root>",
LAST);
		lr_end_sub_transaction("inQuickLink_aspx", LR_AUTO);


		lr_start_sub_transaction("uiHomePage_aspx", "OOTB_LoginSubmit");
			web_custom_request("uiHomePage.aspx",
				"URL=http://{server_server}/core/ui/uiHomePageData.aspx?rId={{rid}}&sno={{sno}}&ui=W&Action=GetCustomLayout&ResourceId={b67b0189-674e-4e77-b976-d418beacb42f}&PortalTemplateId=d0deee78-cc4b-46bc-8b63-7475ad443a5a",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/Home.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("uiHomePage_aspx", LR_AUTO);


		lr_start_sub_transaction("uiHomePageData_aspx", "OOTB_LoginSubmit");
			web_custom_request("uiHomePageData.aspx",
				"URL=http://{server_server}/Core/UI/uiHomePageData.aspx?rId={{rid}}&sno={{sno}}&ui=W&Action=GetCustomLayout&ResourceId={{rid}}&PortalTemplateId=d0deee78-cc4b-46bc-8b63-7475ad443a5a",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiHomePage.aspx?rId={{rid}}&sno={{sno}}&ui=W&portalTemplateId=d0deee78-cc4b-46bc-8b63-7475ad443a5ae",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("uiHomePageData_aspx", LR_AUTO);


	lr_end_transaction("OOTB_LoginSubmit",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("CUSTOM_OpenReports");
		lr_start_sub_transaction("UtilityPostXml_aspx", "CUSTOM_OpenReports");
			web_custom_request("UtilityPostXml.aspx",
				"URL=http://{server_server}/Utility/UtilityPostXml.aspx?rid={{rid}}&sno={{sno}}&ui=W&Action=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiDirectory.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				"Body=VRP",
LAST);
		lr_end_sub_transaction("UtilityPostXml_aspx", LR_AUTO);


		lr_start_sub_transaction("tvReportTree_asp", "CUSTOM_OpenReports");
			web_url("tvReportTree.asp",
				"URL=http://{server_server}/Core/TreeViews/tvReportTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiDirectory.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvReportTree_asp", LR_AUTO);


		lr_start_sub_transaction("tvReport_aspx", "CUSTOM_OpenReports");
			web_url("tvReport.aspx",
				"URL=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReportTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvReport_aspx", LR_AUTO);


	lr_end_transaction("CUSTOM_OpenReports",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_ExpandCustomReports");
		lr_start_sub_transaction("ifrtvReport_aspx", "CUSTOM_ExpandCustomReports");
			web_url("ifrtvReport.aspx",
				"URL=http://{server_server}/Core/Treeviews/ifrtvReport.aspx?cid=tvEngRep&rid={{rid}}&sno={{sno}}&ui=W&cptype=ReportCat_ex&id=58444c72-0caa-4616-94a6-67d397bff898&offset=120",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvReport_aspx", LR_AUTO);


	lr_end_transaction("CUSTOM_ExpandCustomReports",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_OpenOrderUnprocessing");
		lr_start_sub_transaction("LoadReportData_aspx", "CUSTOM_OpenOrderUnprocessing");
			web_custom_request("LoadReportData.aspx",
				"URL=http://{server_server}/Core/Treeviews/LoadReportData.aspx?rid={{rid}}&sno={{sno}}&ui=W&id={205b317e-ede9-4a24-a945-d7ac77392178}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("LoadReportData_aspx", LR_AUTO);


		lr_start_sub_transaction("OrderUnprocessing_aspx", "CUSTOM_OpenOrderUnprocessing");
			web_url("OrderUnprocessing.aspx",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderUnprocessing.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("OrderUnprocessing_aspx", LR_AUTO);


	lr_end_transaction("CUSTOM_OpenOrderUnprocessing",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_SubmitOrderUnprocessing");
		lr_start_sub_transaction("OrderUnprocessing_aspx_2", "CUSTOM_SubmitOrderUnprocessing");
			web_submit_data("OrderUnprocessing.aspx_2",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderUnprocessing.aspx?rid=%7b{rid}%7d&sno=%7b{sno}%7d&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderUnprocessing.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=ctl00$scriptManager", "Value=ctl00$ContentPlaceHolder1$upnlDelivery|ctl00$ContentPlaceHolder1$btnSubmit", ENDITEM,
				"Name=__EVENTTARGET", "Value=ctl00$ContentPlaceHolder1$btnSubmit", ENDITEM,
				"Name=__EVENTARGUMENT", "Value=", ENDITEM,
				"Name=VIEWSTATE", "Value={viewstate1}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={eventvalidation1}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlBusinessUnit", "Value=11", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtOrderNumber", "Value={ordercustomerbusinessid_ordernumber}", ENDITEM,
				"Name=__ASYNCPOST", "Value=true", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderUnprocessing_aspx_2", LR_AUTO);


		lr_start_sub_transaction("OrderUnprocessing_aspx_3", "CUSTOM_SubmitOrderUnprocessing");
			web_submit_data("OrderUnprocessing.aspx_3",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderUnprocessing.aspx?rid=%7b{rid}%7d&sno=%7b{sno}%7d&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderUnprocessing.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=ctl00$scriptManager", "Value=ctl00$ContentPlaceHolder1$upnlDelivery|ctl00$ContentPlaceHolder1$btnSubmit", ENDITEM,
				"Name=__EVENTTARGET", "Value=ctl00$ContentPlaceHolder1$btnSubmit", ENDITEM,
				"Name=__EVENTARGUMENT", "Value=", ENDITEM,
				"Name=__VIEWSTATE", "Value={viewstate1}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={eventvalidation1}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlBusinessUnit", "Value=11", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtOrderNumber", "Value={ordercustomerbusinessid_ordernumber}", ENDITEM,
				"Name=__ASYNCPOST", "Value=Value=true", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderUnprocessing_aspx_3", LR_AUTO);


	lr_end_transaction("CUSTOM_SubmitOrderUnprocessing",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("CUSTOM_OpenReports");
		lr_start_sub_transaction("UtilityPostXml_aspx_2", "CUSTOM_OpenReports");
			web_custom_request("UtilityPostXml.aspx_2",
				"URL=http://{server_server}/Utility/UtilityPostXml.aspx?rid={{rid}}&sno={{sno}}&ui=W&Action=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiDirectory.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				"Body=VRP",
LAST);
		lr_end_sub_transaction("UtilityPostXml_aspx_2", LR_AUTO);


		lr_start_sub_transaction("tvReportTree_asp_2", "CUSTOM_OpenReports");
			web_url("tvReportTree.asp_2",
				"URL=http://{server_server}/Core/TreeViews/tvReportTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiDirectory.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvReportTree_asp_2", LR_AUTO);


		lr_start_sub_transaction("tvReport_aspx_5", "CUSTOM_OpenReports");
			web_url("tvReport.aspx_5",
				"URL=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReportTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvReport_aspx_5", LR_AUTO);


	lr_end_transaction("CUSTOM_OpenReports",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_ExpandCustomReports");
		lr_start_sub_transaction("ifrtvReport_aspx_2", "CUSTOM_ExpandCustomReports");
			web_url("ifrtvReport.aspx_2",
				"URL=http://{server_server}/Core/Treeviews/ifrtvReport.aspx?cid=tvEngRep&rid={{rid}}&sno={{sno}}&ui=W&cptype=ReportCat_ex&id=58444c72-0caa-4616-94a6-67d397bff898&offset=180",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvReport_aspx_2", LR_AUTO);


	lr_end_transaction("CUSTOM_ExpandCustomReports",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_OpenOrderAssocation");
		lr_start_sub_transaction("LoadReportData_aspx_2", "CUSTOM_OpenOrderAssocation");
			web_custom_request("LoadReportData.aspx_2",
				"URL=http://{server_server}/Core/Treeviews/LoadReportData.aspx?rid={{rid}}&sno={{sno}}&ui=W&id={2dd3cc59-f98d-45ff-8c34-9b40d94bc4b9}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewReport",
				"Mode=HTTP",
LAST);
			web_add_cookie("s_hwp=brbsdt1%7C%7Cnull%7C%7C10%3A9%3A2013%3A10%3A33%7C%7CY%7C%7CY%7C%7C332-1393%7C%7CNaN%7C%7Cv270aw11a%7C%7Cnull%7C%7CY%7C%7Cv270aw11a%2C1%2C1119%7C%7Cc2%7C%7C13kw8s1; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("s_vi=[CS]v1\|28D2A13C851D12AB-4000012E4000F264[CE]; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("GAFed_Identity=9dphbC5OzAb9/BqDJj8tcMxXWRLst0WmZxq4KJYbX3SJVEmM7YycNuggB5/piPUW0E2vrTzeDGyHAZSvIyVpNg==; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("eSupportSegment=s=BIZ&c=br&l=pt&cs=brdhs1; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("OLRProduct=OLRProduct=13KW8S1\|; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("s_sv_112_p1=1@18@s/13115/11194&e/57; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("GAAnon=f053f3d5-8ab3-49db-8766-bb51f8c5a880; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("sdc=st=13KW8S1; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("SITESERVER=ID=2ea0efc6251e44449f0750e4979da347; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("StormPCookie=js=1&pl=en&pc=us&bandwidth=NA&fstn=Alex&lstn=Olicheski; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("s_vnum=1402081369524%26vn%3D3; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("WT_FPC=id=10.150.252.103-2487145696.30284599:lv=1380275759527:ss=1380275759527; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("RBI=brdhsbrdhs1=snp:332-1036:8d047651332ee97/");
			web_add_cookie("snp:332-1392:8d04764cce0a8c3/");
			web_add_cookie("snp:332-1393:8d04765567ff472; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("RBI_RPI="   "%7B%22rbi%22%3A%7B%22systems%22%3A%5B%5D%2C%22snp%22%3A%5B%7B%22id%22%3A%22332-1392%22%2C%22url%22%3A%22http%3A%2F%2Faccessories.dell.com%2Fsna%2Fproductdetail.aspx%3Fc%3Dbr%26l%3Dpt%26s%3Ddhs%26cs%3Dbrdhs1%26sku%3D332-1392%22%2C%22timestamp%22%3A1373025774305%7D%2C%7B%22id%22%3A%22332-1036%22%2C%22url%22%3A%22http%3A%2F%2Faccessories.dell.com%2Fsna%2Fproductdetail.aspx%3Fc%3Dbr%26l%3Dpt%26s%3Ddhs%26cs%3Dbrdhs1%26sku%3D332-1036%22%2C%22timestamp%22%3A1373025892261%7D%2C%7B%22id%22%3A%22332-1393%22"   "%2C%22url%22%3A%22http%3A%2F%2Faccessories.dell.com%2Fsna%2Fproductdetail.aspx%3Fc%3Dbr%26l%3Dpt%26s%3Ddhs%26cs%3Dbrdhs1%26sku%3D332-1393%22%2C%22timestamp%22%3A1373026005445%7D%5D%2C%22country%22%3A%22br%22%2C%22language%22%3A%22pt%22%2C%22cs%22%3A%22brdhs1%22%7D%2C%22rpi%22%3A%7B%22systems%22%3A%5B%7B%22id%22%3A%22v270aw11a%22%2C%22url%22%3A%22http%3A%2F%2Fconfigure.dell.com%2Fdellstore%2Fconfig.aspx%3Foc%3Dv270aw11a%26c%3Dbr%26l%3Dpt%26s%3Dbsd%26cs%3Dbrbsdt1%22%2C%22timestamp%22%3A1378819962396"   "%2C%22qty%22%3A%221%22%2C%22price%22%3A1119%2C%22pvid%22%3A%22vostro-270s%22%7D%5D%2C%22snp%22%3A%5B%5D%2C%22country%22%3A%22br%22%2C%22language%22%3A%22pt%22%2C%22cs%22%3A%22brbsdt1%22%7D%7D; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie(""GACold=Tvgal00EP1v9IPGQpSRv7v7bnKFraSSIfscA8EYGgXrxvMv0MPY0Ol1d+qAdTMPFiniUTiYQHUhGu03Qg0s7U8dsgYddMO2NVgfl8ke7ry9UZjaGou2zmKHJ+QFFyk9+4ifP0MXN3pqrVwSs9fChbZg+SE8OssX3IgaJiUh1/SvcpiP4Vzk7jXtynS41TFu8dH2WTsq1F3AofKRX/v/iSkKjXpwrQvFJ1RmtITJY1sLFJ5qRwQDRzE57IiyRP6EBqdNtWcu5cnMAlLbZdgKheiQaPB3MDgX5cvDidAzOLyFEoePyRw82y+mamTghoe4oJy9SQDhkcBW67G6dr8kiMftZDqNSd3V/BdcsaY40JvVsa1sTiYHJBtptdcydwMeDZdUxScOM6TtNUlw5ZxaqZVTvXCT2PB1e6KpQmwrTykRCpTadc7U3btnFMShuNei8; DOMAIN="   "dc-changepoint-perf.3dnsqa.dell.com");
			web_add_cookie("Profile=a33fdb21-2c90-49cb-ac83-2373b02c7d25; DOMAIN=dc-changepoint-perf.3dnsqa.dell.com");
		lr_end_sub_transaction("LoadReportData_aspx_2", LR_AUTO);


		lr_start_sub_transaction("OrderFiltering_aspx", "CUSTOM_OpenOrderAssocation");
			web_url("OrderFiltering.aspx",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/TreeViews/tvReport.aspx?rid={{rid}}&sNo={{sno}}&ui=W&ViewOption=ViewCat",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("OrderFiltering_aspx", LR_AUTO);


	lr_end_transaction("CUSTOM_OpenOrderAssocation",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("CUSTOM_SearchByCustomerData");
		lr_start_sub_transaction("OrderFiltering_aspx_2", "CUSTOM_SearchByCustomerData");
			web_submit_data("OrderFiltering.aspx_2",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid=%7b{rid}%7d&sno=%7b{sno}%7d&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=ctl00$scriptManager", "Value=ctl00$ContentPlaceHolder1$uppOrderFiltering|ctl00$ContentPlaceHolder1$rbnCustomerData", ENDITEM,
				"Name=__LASTFOCUS", "Value=", ENDITEM,
				"Name=__EVENTTARGET", "Value=ctl00$ContentPlaceHolder1$rbnCustomerData", ENDITEM,
				"Name=__EVENTARGUMENT", "Value=", ENDITEM,
				"Name=__VIEWSTATE", "Value={vs_341}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={evt_val43}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1${rbnChannelData44}", "Value={rbnchanneldata44}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$regionValidatorExtender_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$serviceTowerValidatorExtender_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$rbnCustomerData", "Value=rbnCustomerData", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtShipToCustomer", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtOrderNumber", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlBusinessUnit", "Value=4747", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$grpDate", "Value=rbtnAll", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtStartDate", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtStartDateMask_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$vceRequiredStartDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderValidStartDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtEndDate", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtEndDateMask_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$vceRequiredEndDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderDateRange_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderValidEndDate_ClientState", "Value=", ENDITEM,
				"Name=__ASYNCPOST", "Value=true", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderFiltering_aspx_2", LR_AUTO);


		lr_start_sub_transaction("OrderFiltering_aspx_3", "CUSTOM_SearchByCustomerData");
			web_submit_data("OrderFiltering.aspx_3",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid=%7b{rid}%7d&sno=%7b{sno}%7d&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=ctl00$scriptManager", "Value=ctl00$ContentPlaceHolder1$upnlDateRange|ctl00$ContentPlaceHolder1$btnGo", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$regionValidatorExtender_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$serviceTowerValidatorExtender_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$rbnCustomerData", "Value=rbnCustomerData", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtShipToCustomer", "Value={ordercustomerbusinessid_ordernumber}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtOrderNumber", "Value={ordercustomerbusinessid_order}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlBusinessUnit", "Value=11", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$grpDate", "Value=rbtnAll", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtStartDate", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtStartDateMask_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$vceRequiredStartDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderValidStartDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtEndDate", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtEndDateMask_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$vceRequiredEndDate_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderDateRange_ClientState", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ValidatorCalloutExtenderValidEndDate_ClientState", "Value=", ENDITEM,
				"Name=__LASTFOCUS", "Value=", ENDITEM,
				"Name=__EVENTTARGET", "Value=", ENDITEM,
				"Name=_EVENTARGUMENT", "Value=", ENDITEM,
				"Name=__VIEWSTATE", "Value={vs_new53}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={evt_val_255}", ENDITEM,
				"Name=__ASYNCPOST", "Value=true", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$btnGo", "Value=Go", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderFiltering_aspx_3", LR_AUTO);


		lr_start_sub_transaction("OrderToRequestFilterResults_aspx", "CUSTOM_SearchByCustomerData");
			web_url("OrderToRequestFilterResults.aspx",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid={rid}&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderFiltering.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("OrderToRequestFilterResults_aspx", LR_AUTO);


	lr_end_transaction("CUSTOM_SearchByCustomerData",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("CUSTOM_AssociateSelectedOrders");
		lr_start_sub_transaction("OrderToRequestFilterResults_aspx_2", "CUSTOM_AssociateSelectedOrders");
			web_submit_data("OrderToRequestFilterResults.aspx_2",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Mode=HTTP",
				ITEMDATA,
				"Name=__EVENTTARGET", "Value=", ENDITEM,
				"Name=__EVENTARGUMENT", "Value=", ENDITEM,
				"Name=__VIEWSTATE", "Value={vs_364}", ENDITEM,
				"Name=__VIEWSTATEENCRYPTED", "Value=", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={evt_val66}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$btnAssociateOrders", "Value=Associate Selected Orders", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$lvwCustomers$ctrl0$lvwOrders$ctrl0$chkSelectOrder", "Value=on", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$lvwCustomers$ctrl0$lvwOrders$ctrl0$cpeCustomers_ClientState", "Value=true", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderToRequestFilterResults_aspx_2", LR_AUTO);


	lr_end_transaction("CUSTOM_AssociateSelectedOrders",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("CUSTOM_FillRFS");
		lr_start_sub_transaction("OrderToRequestAssociation_aspx", "CUSTOM_FillRFS");
			web_custom_request("OrderToRequestAssociation.aspx",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestAssociation.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={orderordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Mode=HTTP",
				"Body=ctl00%24scriptManager=ctl00%24ContentPlaceHolder1%24upnlTop%7Cctl00%24ContentPlaceHolder1%24ddlRequestType&__EVENTTARGET=ctl00%24ContentPlaceHolder1%24ddlRequestType&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE={vs_372_url}&__EVENTVALIDATION={evt_val74_url}&ctl00%24ContentPlaceHolder1%24hdnOrderCount=1&ctl00%24ContentPlaceHolder1%24ddlRequestType=RFS&ctl00%24ContentPlaceHolder1%24ddlDealType=0&ctl00%24ContentPlaceHolder1%24txtRequestNumber=&ctl00%24ContentPlaceHolder1%24txtSolutionType=&"   "ctl00%24ContentPlaceHolder1%24ddlOMSystem=0018c2fd-a87d-40b7-abcb-35dd94b18ee8&ctl00%24ContentPlaceHolder1%24txtCustomerNumber={ordercustomerbusinessid_ordernumber}&ctl00%24ContentPlaceHolder1%24txtCustomerName=STERLING%20COMPUTERS&ctl00%24ContentPlaceHolder1%24ddlRegion=ABU&ctl00%24ContentPlaceHolder1%24ddlServiceTower={ordercustomerbusinessid_servicetower}&ctl00%24ContentPlaceHolder1%24ddlBusinessUnit=11&ctl00%24ContentPlaceHolder1%24ddlBillingOffice=0&ctl00%24ContentPlaceHolder1%24ddlBillingMethod=45a14add-8f84-4558-ab85-30cb0e5946bf&"   "ctl00%24ContentPlaceHolder1%24ddlBillingType=0&ctl00%24ContentPlaceHolder1%24ddlExpenseBilling=0&ctl00%24ContentPlaceHolder1%24txtBillDate=11%2F29%2F2012&ctl00%24ContentPlaceHolder1%24txtSFDCNumber=&ctl00%24ContentPlaceHolder1%24txtContractStartDate=&ctl00%24ContentPlaceHolder1%24txtContractEndDate=&ctl00%24ContentPlaceHolder1%24ddlProduct=0&ctl00%24ContentPlaceHolder1%24txtComments=&ctl00%24ContentPlaceHolder1%24txtSolutionArchitect=&ctl00%24ContentPlaceHolder1%24txtSProjectTemplate=&"   "ctl00%24ContentPlaceHolder1%24acProjectManager%24txtCompletionBox=&ctl00%24ContentPlaceHolder1%24ddlDProjectTemplate=0&ctl00%24ContentPlaceHolder1%24acWorkCodeCategory%24txtCompletionBox=&ctl00%24ContentPlaceHolder1%24acWorkCode%24txtCompletionBox=&__ASYNCPOST=true&",
LAST);
		lr_end_sub_transaction("OrderToRequestAssociation_aspx", LR_AUTO);


		lr_start_sub_transaction("OrderToRequestAssociation_aspx_2", "CUSTOM_FillRFS");
			web_custom_request("OrderToRequestAssociation.aspx_2",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestAssociation.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Mode=HTTP",
				"Body=ctl00%24scriptManager=ctl00%24ContentPlaceHolder1%24upnlTop%7Cctl00%24ContentPlaceHolder1%24ddlDealType&ctl00%24ContentPlaceHolder1%24hdnOrderCount=1&ctl00%24ContentPlaceHolder1%24ddlRequestType=RFS&ctl00%24ContentPlaceHolder1%24ddlDealType=Standard&ctl00%24ContentPlaceHolder1%24txtRequestNumber=&ctl00%24ContentPlaceHolder1%24txtSolutionType=&ctl00%24ContentPlaceHolder1%24ddlOMSystem=0018c2fd-a87d-40b7-abcb-35dd94b18ee8&ctl00%24ContentPlaceHolder1%24txtCustomerNumber={ordercustomerbusinessid_ordernumber}&"   "ctl00%24ContentPlaceHolder1%24txtCustomerName=STERLING%20COMPUTERS&ctl00%24ContentPlaceHolder1%24ddlRegion=ABU&ctl00%24ContentPlaceHolder1%24ddlServiceTower={ordercustomerbusinessid_servicetower}&ctl00%24ContentPlaceHolder1%24ddlBusinessUnit=11&ctl00%24ContentPlaceHolder1%24ddlBillingOffice=0&ctl00%24ContentPlaceHolder1%24ddlBillingMethod=45a14add-8f84-4558-ab85-30cb0e5946bf&ctl00%24ContentPlaceHolder1%24ddlBillingType=0&ctl00%24ContentPlaceHolder1%24ddlExpenseBilling=0&ctl00%24ContentPlaceHolder1%24txtBillDate=11%2F29%2F2012&"   "ctl00%24ContentPlaceHolder1%24txtSFDCNumber=&ctl00%24ContentPlaceHolder1%24txtContractStartDate=&ctl00%24ContentPlaceHolder1%24txtContractEndDate=&ctl00%24ContentPlaceHolder1%24ddlProduct=0&ctl00%24ContentPlaceHolder1%24txtComments=&ctl00%24ContentPlaceHolder1%24txtSolutionArchitect=&ctl00%24ContentPlaceHolder1%24txtSProjectTemplate=&ctl00%24ContentPlaceHolder1%24acProjectManager%24txtCompletionBox=&ctl00%24ContentPlaceHolder1%24ddlDProjectTemplate=0&"   "ctl00%24ContentPlaceHolder1%24acWorkCodeCategory%24txtCompletionBox=&ctl00%24ContentPlaceHolder1%24acWorkCode%24txtCompletionBox=&__EVENTTARGET=ctl00%24ContentPlaceHolder1%24ddlDealType&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE={vs_new83_url}&__EVENTVALIDATION={evt_val_285_url}&__ASYNCPOST=true&",
LAST);
		lr_end_sub_transaction("OrderToRequestAssociation_aspx_2", LR_AUTO);


		lr_start_sub_transaction("OrderToRequestAssociation_aspx_3", "CUSTOM_FillRFS");
			web_custom_request("OrderToRequestAssociation.aspx_3",
				"URL=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestAssociation.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Mode=HTTP",
				"Body=ctl00%24scriptManager=ctl00%24ContentPlaceHolder1%24acProjectManager%24uppAutoComplete%7Cctl00%24ContentPlaceHolder1%24acProjectManager%24btnUpdate&ctl00%24ContentPlaceHolder1%24hdnOrderCount=1&ctl00%24ContentPlaceHolder1%24ddlRequestType=RFS&ctl00%24ContentPlaceHolder1%24ddlDealType=Standard&ctl00%24ContentPlaceHolder1%24txtRequestNumber=&ctl00%24ContentPlaceHolder1%24txtSolutionType=PERF&ctl00%24ContentPlaceHolder1%24ddlOMSystem=0018c2fd-a87d-40b7-abcb-35dd94b18ee8&"   "ctl00%24ContentPlaceHolder1%24txtCustomerNumber={ordercustomerbusinessid_ordernumber}&ctl00%24ContentPlaceHolder1%24txtCustomerName=STERLING%20COMPUTERS&ctl00%24ContentPlaceHolder1%24ddlRegion=ABU&ctl00%24ContentPlaceHolder1%24ddlServiceTower={ordercustomerbusinessid_servicetower}&ctl00%24ContentPlaceHolder1%24ddlBusinessUnit=11&ctl00%24ContentPlaceHolder1%24ddlBillingOffice=5c952532-dc08-42bb-8b83-4fba0559fb20&ctl00%24ContentPlaceHolder1%24ddlBillingMethod=45a14add-8f84-4558-ab85-30cb0e5946bf&ctl00%24ContentPlaceHolder1%24ddlBillingType=0&"   "ctl00%24ContentPlaceHolder1%24ddlExpenseBilling=0&ctl00%24ContentPlaceHolder1%24txtBillDate=11%2F29%2F2012&ctl00%24ContentPlaceHolder1%24txtSFDCNumber=Default%20Deal%20Id&ctl00%24ContentPlaceHolder1%24txtContractStartDate=01%2F01%2F2013&ctl00%24ContentPlaceHolder1%24txtContractEndDate=01%2F01%2F2040&ctl00%24ContentPlaceHolder1%24ddlProduct=52855cc4-e2f8-4814-85e1-5d05b3ae77ab&ctl00%24ContentPlaceHolder1%24txtComments=PERFORMANCE TESTING&ctl00%24ContentPlaceHolder1%24txtSolutionArchitect=&"   "ctl00%24ContentPlaceHolder1%24txtSProjectTemplate=&ctl00%24ContentPlaceHolder1%24acProjectManager%24txtCompletionBox=alex%20tarnowski&ctl00%24ContentPlaceHolder1%24ddlDProjectTemplate=0&ctl00%24ContentPlaceHolder1%24acWorkCodeCategory%24txtCompletionBox=&ctl00%24ContentPlaceHolder1%24acWorkCode%24txtCompletionBox=&__EVENTTARGET=ctl00%24ContentPlaceHolder1%24acProjectManager%24btnUpdate&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE={vs_new91_url}&__EVENTVALIDATION={evt_val_293_url}&__ASYNCPOST=true&",
LAST);
		lr_end_sub_transaction("OrderToRequestAssociation_aspx_3", LR_AUTO);


	lr_end_transaction("CUSTOM_FillRFS",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("CUSTOM_SaveRFS");
		lr_start_sub_transaction("OrderToRequestAssociation_aspx_4", "CUSTOM_SaveRFS");
			web_submit_data("OrderToRequestAssociation.aspx_4",
				"Action=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestAssociation.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://dc-changepoint-perf.3dnsqa.dell.com:100/CustomIntegration/OrderManagement/OrderToRequestFilterResults.aspx?StartDate=&EndDate=&RegionCodes=&CountryCodes=&ServiceTowerAbbreviations=&SegmentCodes=&CustomerNumber={ordercustomerbusinessid_ordernumber}&BusinessUnit=UnitedStates&rid=b67b0189-674e-4e77-b976-d418beacb42f&OrderNumber={ordercustomerbusinessid_order}&IncludeBAROrders=1&PageNumber=0&Feature=&IncludeCancelledOrders=False",
				"Mode=HTTP",
				ITEMDATA,
				"Name=ctl00$ContentPlaceHolder1$hdnOrderCount", "Value={ctl00_contentplaceholder1_hdnordercount75}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlRequestType", "Value=RFS", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlDealType", "Value=Standard", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtRequestNumber", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtSolutionType", "Value=PERF_SOLUTION_{random}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtCustomerNumber", "Value={ordercustomerbusinessid_ordernumber}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtCustomerName", "Value={ordercustomerbusinessid_customername}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlServiceTower", "Value={ordercustomerbusinessid_servicetower}", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlBillingType", "Value=0", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlExpenseBilling", "Value=0", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtContractStartDate", "Value=1/1/2013", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtContractEndDate", "Value=1/1/2040", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlProduct", "Value=52855cc4-e2f8-4814-85e1-5d05b3ae77ab", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtComments", "Value=PERFORMANCE TESTING", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtSolutionArchitect", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$txtSProjectTemplate", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$acProjectManager$txtCompletionBox", "Value=alex tarnowski", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$acProjectManager$ddlSugestionBox", "Value=b67b0189-674e-4e77-b976-d418beacb42f", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$ddlDProjectTemplate", "Value=c7519daf-77b0-4fa8-89ea-cf9743d42660", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$acWorkCodeCategory$txtCompletionBox", "Value=", ENDITEM,
				"Name=ctl00$ContentPlaceHolder1$acWorkCode$txtCompletionBox", "Value=", ENDITEM,
				"Name=__EVENTTARGET", "Value=ctl00$ContentPlaceHolder1$btnSave", ENDITEM,
				"Name=__EVENTARGUMENT", "Value=", ENDITEM,
				"Name=__LASTFOCUS", "Value=__VIEWSTATE@@{vs_new99}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={evt_val_2101}", ENDITEM,
				LAST);
		lr_end_sub_transaction("OrderToRequestAssociation_aspx_4", LR_AUTO);


	lr_end_transaction("CUSTOM_SaveRFS",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("OOTB_Logout");
		lr_start_sub_transaction("fslogout_asp", "OOTB_Logout");
			web_url("fslogout.asp",
				"URL=http://{server_server}/login/fslogout.asp?rid={{rid}}&sno={{sno}}&ui=W&home=Yes",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("fslogout_asp", LR_AUTO);


	lr_end_transaction("OOTB_Logout",LR_AUTO);


	lr_end_transaction("uc_custom_orderassociation_and_unprocessing",LR_AUTO);

	return 0;
}
