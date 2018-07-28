Action()
{
web_set_max_html_param_len("9999999");
lr_start_transaction("uc_ootb_wf_assigndelivery");


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


lr_start_transaction("dshReminder_asp");
web_url("dshReminder.asp",
"URL=http://{server_server}/core/ui/dshReminder.asp?rid={{rid}}&sno={sno}&ui=W&home=Yes",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/Core/UI/uiHomePage.aspx?rId={{rid}}&sno={sno}&ui=W&portalTemplateId=30d45dcd-cb3b-4232-8c08-f95e5a5dd9d7&isUserDefined=True",
				"Mode=HTTP",
LAST);


lr_end_transaction("dshReminder_asp",LR_AUTO);
lr_think_time(15);
lr_start_transaction("rpWorkflow_aspx");
web_url("rpWorkflow.aspx",
"URL=http://{server_server}/core/ui/rpWorkflow.aspx?rid={{rid}}&sno={sno}&ui=W&entity=REQUEST",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
LAST);


lr_end_transaction("rpWorkflow_aspx",LR_AUTO);
lr_think_time(15);
	lr_think_time(15);


	lr_start_transaction("OOTB_OpenRFS");
		lr_start_sub_transaction("Request", "OOTB_OpenRFS");
			web_url("Request",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/MasterPageView/Request",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("Request", LR_AUTO);


		lr_start_sub_transaction("UDFLayoutREQPCON0", "OOTB_OpenRFS");
			web_url("UDFLayoutREQPCON0",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/MasterPageView/Request",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("UDFLayoutREQPCON0", LR_AUTO);


		lr_start_sub_transaction("UDFLayoutREQPCON1", "OOTB_OpenRFS");
			web_url("UDFLayoutREQPCON1",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/UDF/UDFLayoutREQPCON1",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("UDFLayoutREQPCON1", LR_AUTO);


		lr_start_sub_transaction("UDFLayoutREQPCON2", "OOTB_OpenRFS");
			web_url("UDFLayoutREQPCON2",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/UDF/UDFLayoutREQPCON2",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("UDFLayoutREQPCON2", LR_AUTO);


		lr_start_sub_transaction("UDFLayoutREQPCON3", "OOTB_OpenRFS");
			web_url("UDFLayoutREQPCON3",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/UDF/UDFLayoutREQPCON3",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("UDFLayoutREQPCON3", LR_AUTO);


		lr_start_sub_transaction("inWorkFlowData_aspx", "OOTB_OpenRFS");
			web_url("inWorkFlowData.aspx",
				"URL=http://{server_server}/WorkflowManagement/Entity/inWorkFlowData.aspx?rid={{rid}}&sno={sno}&ui=W&entity=Request&entityid={entityid}&wff=1&wfe=1&isNet=0",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("inWorkFlowData_aspx", LR_AUTO);


	lr_end_transaction("OOTB_OpenRFS",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("OOTB_EditRFS");
		lr_start_sub_transaction("DialogPresentationSrv_aspx", "OOTB_EditRFS");
			web_custom_request("DialogPresentationSrv.aspx",
				"URL=http://{server_server}/MasterPages/DialogPresentationSrv.aspx?rid={{rid}}&sno={sno}&ui=W&code=REQ",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/HelpDesk/frRequestInfo.aspx?reqId={{entityid}}&rId={{rid}}&sno={sno}&ui=W",
				"Mode=HTTP",
				"Body=<root/>",
LAST);
		lr_end_sub_transaction("DialogPresentationSrv_aspx", LR_AUTO);


		lr_start_sub_transaction("dcRequest_aspx", "OOTB_EditRFS");
			web_url("dcRequest.aspx",
				"URL=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("dcRequest_aspx", LR_AUTO);


		lr_start_sub_transaction("Request_2", "OOTB_EditRFS");
			web_url("Request_2",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{{rid}}/MasterPageTabbed/Request",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("Request_2", LR_AUTO);


		lr_start_sub_transaction("RequestListsXML_aspx", "OOTB_EditRFS");
			web_custom_request("RequestListsXML.aspx",
				"URL=http://{server_server}/HelpDesk/RequestListsXML.aspx?rid={{rid}}&sno={sno}&ui=W&criteria=&sourceType=Initiator&langId=E&codeId=USA&sValue=D88620E1-2B6D-4851-AD9B-77864D16CC4C&CustomerId=06CF3214-364A-4BCE-99CD-525C58A88062&ContactOnly=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root></root>\r\n",
LAST);
		lr_end_sub_transaction("RequestListsXML_aspx", LR_AUTO);


		lr_start_sub_transaction("RequestListsXML_aspx_2", "OOTB_EditRFS");
			web_custom_request("RequestListsXML.aspx_2",
				"URL=http://{server_server}/HelpDesk/RequestListsXML.aspx?rid={{rid}}&sno={sno}&ui=W&criteria=&sourceType=Customer&langId=E&codeId=USA&sValue=06CF3214-364A-4BCE-99CD-525C58A88062&InitiatorType=-2&InitiatorId=D88620E1-2B6D-4851-AD9B-77864D16CC4C&ProjectId=5BA5960E-6820-4CE1-AB8F-191506F1B38D",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root></root>\r\n",
LAST);
		lr_end_sub_transaction("RequestListsXML_aspx_2", LR_AUTO);


		lr_start_sub_transaction("RequestListsXML_aspx_3", "OOTB_EditRFS");
			web_custom_request("RequestListsXML.aspx_3",
				"URL=http://{server_server}/HelpDesk/RequestListsXML.aspx?rid={{rid}}&sno={sno}&ui=W&criteria=&sourceType=Project&langId=E&codeId=USA&sValue=5BA5960E-6820-4CE1-AB8F-191506F1B38D&EngagementId=020ea6aa-938f-40f4-9a74-4d611bb0fbae&CustomerId=06CF3214-364A-4BCE-99CD-525C58A88062",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root></root>\r\n",
LAST);
		lr_end_sub_transaction("RequestListsXML_aspx_3", LR_AUTO);


		lr_start_sub_transaction("RequestListsXML_aspx_4", "OOTB_EditRFS");
			web_custom_request("RequestListsXML.aspx_4",
				"URL=http://{server_server}/HelpDesk/RequestListsXML.aspx?rid={{rid}}&sno={sno}&ui=W&criteria=&sourceType=Assignment&langId=E&codeId=USA&sValue=5AD8024A-AF7C-477D-A687-3FC5D6595C53&RequestId={{entityid}}&HelpDeskId=0fd91759-8c51-11d3-8aa6-0060975aec0f&ProjectId=5BA5960E-6820-4CE1-AB8F-191506F1B38D",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root></root>\r\n",
LAST);
		lr_end_sub_transaction("RequestListsXML_aspx_4", LR_AUTO);


		lr_start_sub_transaction("RecordLock_aspx", "OOTB_EditRFS");
			web_custom_request("RecordLock.aspx",
				"URL=http://{server_server}/Core/Locking/RecordLock.aspx?rId={{rid}}&sno={sno}&ui=W&LockAction=AQUIRELOCK&RecordType=REQ&RecordId={{entityid}}&Description=RFS-2011-25021",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("RecordLock_aspx", LR_AUTO);


		lr_start_sub_transaction("ifrKeepLockAliveOrRelease_asp", "OOTB_EditRFS");
			web_url("ifrKeepLockAliveOrRelease.asp",
				"URL=http://{server_server}/helpdesk/ifrKeepLockAliveOrRelease.asp?rid={{rid}}&sno={sno}&ui=W&layout=TABBED&RequestId={{entityid}}&recordtype=REQ&recordId={{entityid}}&createdBy={{rid}}&requestNum=RFS-2011-25021",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrKeepLockAliveOrRelease_asp", LR_AUTO);


		lr_start_sub_transaction("ifrPostGetEngInfo_aspx", "OOTB_EditRFS");
			web_custom_request("ifrPostGetEngInfo.aspx",
				"URL=http://{server_server}/Core/Engagement/ifrPostGetEngInfo.aspx?rId={{rid}}&sno={sno}&ui=W&sFor=customerAddress&customerId=06CF3214-364A-4BCE-99CD-525C58A88062",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("ifrPostGetEngInfo_aspx", LR_AUTO);


		lr_start_sub_transaction("inWorkFlowFilters_aspx", "OOTB_EditRFS");
			web_url("inWorkFlowFilters.aspx",
				"URL=http://{server_server}/includeFiles/inWorkFlowFilters.aspx?action=inital&rid={{rid}}&sno={sno}&ui=W&entity=Request&entityid={{entityid}}&isNet=true",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("inWorkFlowFilters_aspx", LR_AUTO);


		lr_start_sub_transaction("EntityInterAction_aspx", "OOTB_EditRFS");
			web_custom_request("EntityInterAction.aspx",
				"URL=http://{server_server}/workflowManagement/Entity/EntityInterAction.aspx?action=disable&rid={{rid}}&sno={sno}&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root><disableEdit><statedefid>258c52b1-8354-43ae-82fd-434d206e3645</statedefid><entityid>{{entityid}}</entityid><orgstatus>PEN</orgstatus></disableEdit></root>",
LAST);
		lr_end_sub_transaction("EntityInterAction_aspx", LR_AUTO);


	lr_end_transaction("OOTB_EditRFS",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_EditAssignPM");
		lr_start_sub_transaction("UDFDataSrv_aspx", "OOTB_EditAssignPM");
			web_custom_request("UDFDataSrv.aspx",
				"URL=http://{server_server}/UDF/UDFDataSrv.aspx",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<udf><additional><param id=\"triggerId\" value=\"Code1\" /><param id=\"action\" value=\"search\" /><param id=\"search\" value=\"Paul\" /><param id=\"isTarget\" value=\"1\" /><param id=\"isMultiselect\" value=\"0\" /></additional><property id=\"rid\" value=\"{{rid}}\" /><property id=\"Entity\" value=\"6\" /><property id=\"EntityId\" value=\"{{entityid}}\" /><property id=\"Language\" value=\"E\" /><property id=\"ITD\" value=\"0\" /><property "   "id=\"WorkflowEnabled\" value=\"1\" /><property id=\"AdditionalId\" value=\"RFS\" /><property id=\"CodeId\" value=\"USA\" /><property id=\"LCID\" value=\"1033\" /><property id=\"sno\" value=\"{sno}\" /><property id=\"ui\" value=\"0\" /><property id=\"uiCode\" value=\"W\" /><property id=\"EntityName\" value=\"Request\" /><property id=\"isNew\" value=\"0\" /><property id=\"ignoreWarnings\" value=\"0\" /><property id=\"TemplateId\" value=\""   "00000000-0000-0000-0000-000000000000\" /><property id=\"isTemplate\" value=\"0\" /><property id=\"CopyFromEntityId\" value=\"00000000-0000-0000-0000-000000000000\" /><delta><default><fields></fields></default><data><fields></fields></data><meta><fields></fields></meta></delta></udf>",
LAST);
		lr_end_sub_transaction("UDFDataSrv_aspx", LR_AUTO);


		lr_start_sub_transaction("UDFDataSrv_aspx_2", "OOTB_EditAssignPM");
			web_custom_request("UDFDataSrv.aspx_2",
				"URL=http://{server_server}/UDF/UDFDataSrv.aspx",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<udf><additional><param id=\"triggerId\" value=\"Code1\" /><param id=\"action\" value=\"search\" /><param id=\"search\" value=\"Paul%20s\" /><param id=\"isTarget\" value=\"1\" /><param id=\"isMultiselect\" value=\"0\" /></additional><property id=\"rid\" value=\"{{rid}}\" /><property id=\"Entity\" value=\"6\" /><property id=\"entityid\" value=\"{{entityid}}\" /><property id=\"Language\" value=\"E\" /><property id=\"ITD\" value=\"0\" />"     "<property id=\"WorkflowEnabled\" value=\"1\" /><property id=\"AdditionalId\" value=\"RFS\" /><property id=\"CodeId\" value=\"USA\" /><property id=\"LCID\" value=\"1033\" /><property id=\"sno\" value=\"{sno}\" /><property id=\"ui\" value=\"0\" /><property id=\"uiCode\" value=\"W\" /><property id=\"EntityName\" value=\"Request\" /><property id=\"isNew\" value=\"0\" /><property id=\"ignoreWarnings\" value=\"0\" /><property id=\"TemplateId\" value=\""     "00000000-0000-0000-0000-000000000000\" /><property id=\"isTemplate\" value=\"0\" /><property id=\"CopyFromEntityId\" value=\"00000000-0000-0000-0000-000000000000\" /><delta><default><fields></fields></default><data><fields></fields></data><meta><fields></fields></meta></delta></udf>",      Body=<udf><additional><param id=\"triggerId\" value=\"Code1\" /><param id=\"action\" value=\"search\" /><param id=\"search\" value=\"Paul%20s\" /><param id=\"isTarget\" value=\"1\" /><param id=\"isMultiselect\" value=\"0\" /></additional><property id=\"rid\" value=\"{{rid}}\" /><property id=\"Entity\" value=\"6\" /><property id=\"entityid\" value=\"{{entityid}}\" /><property id=\"Language\" value=\"E\" /><property id=\"ITD\" value=\"0\" />"     "<property id=\"WorkflowEnabled\" value=\"1\" /><property id=\"AdditionalId\" value=\"RFS\" /><property id=\"CodeId\" value=\"USA\" /><property id=\"LCID\" value=\"1033\" /><property id=\"sno\" value=\"{sno}\" /><property id=\"ui\" value=\"0\" /><property id=\"uiCode\" value=\"W\" /><property id=\"EntityName\" value=\"Request\" /><property id=\"isNew\" value=\"0\" /><property id=\"ignoreWarnings\" value=\"0\" /><property id=\"TemplateId\" value=\""     "00000000-0000-0000-0000-000000000000\" /><property id=\"isTemplate\" value=\"0\" /><property id=\"CopyFromEntityId\" value=\"00000000-0000-0000-0000-000000000000\" /><delta><default><fields></fields></default><data><fields></fields></data><meta><fields></fields></meta></delta></udf>",
LAST);
		lr_end_sub_transaction("UDFDataSrv_aspx_2", LR_AUTO);


	lr_end_transaction("OOTB_EditAssignPM",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OTB_SaveAssignDeliveryPM");
		lr_start_sub_transaction("EntityInterAction_aspx_2", "OTB_SaveAssignDeliveryPM");
			web_custom_request("EntityInterAction.aspx_2",
				"URL=http://{server_server}/workflowManagement/Entity/EntityInterAction.aspx?action=isenabled&rid={{rid}}&sno={sno}&ui=W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<root><isenabled><entity>Request</entity></isenabled></root>",
LAST);
		lr_end_sub_transaction("EntityInterAction_aspx_2", LR_AUTO);


		lr_start_sub_transaction("UDFDataSrv_aspx_3", "OTB_SaveAssignDeliveryPM");
			web_custom_request("UDFDataSrv.aspx_3",
				"URL=http://{server_server}/UDF/UDFDataSrv.aspx",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body=<udf><additional><param id=\"action\" value=\"save\" /></additional><property id=\"rid\" value=\"{{rid}}\" /><property id=\"Entity\" value=\"6\" /><property id=\"EntityId\" value=\"{{entityid}}\" /><property id=\"Language\" value=\"E\" /><property id=\"ITD\" value=\"0\" /><property id=\"WorkflowEnabled\" value=\"1\" /><property id=\"AdditionalId\" value=\"RFS\" /><property id=\"CodeId\" value=\"USA\" /><property id=\"LCID\" value=\"1033\" /"   "><property id=\"sno\" value=\"{sno}\" /><property id=\"ui\" value=\"0\" /><property id=\"uiCode\" value=\"W\" /><property id=\"EntityName\" value=\"Request\" /><property id=\"isNew\" value=\"0\" /><property id=\"ignoreWarnings\" value=\"0\" /><property id=\"TemplateId\" value=\"00000000-0000-0000-0000-000000000000\" /><property id=\"isTemplate\" value=\"0\" /><property id=\"CopyFromEntityId\" value=\"00000000-0000-0000-0000-000000000000\" /><delta><default><fields></"   "fields></default><data><fields><field id=\"Code1\" value=\"5394F163-67DA-4F67-AD5A-EC824665DCE2%7CPaul%20Sisung\" /></fields></data><meta><fields></fields></meta></delta></udf>",
LAST);
		lr_end_sub_transaction("UDFDataSrv_aspx_3", LR_AUTO);


		lr_start_sub_transaction("dcRequest_aspx_2", "OTB_SaveAssignDeliveryPM");
			web_submit_data("dcRequest.aspx_2",
				"Action=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				ITEMDATA,
				"Name=__VIEWSTATE", "Value={viewstate_form14}", ENDITEM,
				"Name=Master:cphBody:txtTemplateName", "Value=", ENDITEM,
				"Name=RequestInitiator_HidValue", "Value=D88620E1-2B6D-4851-AD9B-77864D16CC4C", ENDITEM,
				"Name=RequestInitiator_HidText", "Value=Pietro Tarragoni", ENDITEM,
				"Name=Master:cphBody:RequestType", "Value=RFS", ENDITEM,
				"Name=RequestCustomer_HidValue", "Value=06CF3214-364A-4BCE-99CD-525C58A88062", ENDITEM,
				"Name=RequestCustomer_HidText", "Value=Dell Inc.", ENDITEM,
				"Name=RequestProduct", "Value=*Not Applicable", ENDITEM,
				"Name=Master:cphBody:RequestEngagement", "Value=020ea6aa-938f-40f4-9a74-4d611bb0fbae", ENDITEM,
				"Name=Master:cphBody:RequestRequestPriority", "Value=Value=0a043812-0e3f-4576-b80a-b9fbb1fe8dfd", ENDITEM,
				"Name=RequestProject_HidValue", "Value=5BA5960E-6820-4CE1-AB8F-191506F1B38D", ENDITEM,
				"Name=RequestProject_HidText", "Value=Order Mgt Project DO NOT EDIT", ENDITEM,
				"Name=RequestOriginatRequest_HidValue", "Value=", ENDITEM,
				"Name=RequestOriginatRequest_HidText", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestQuantity", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestCategory", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestStatus", "Value=PEN", ENDITEM,
				"Name=selWF_RequestStatus", "Value=", ENDITEM,
				"Name=txtWF_RequestStatus", "Value=Solutioning", ENDITEM,
				"Name=Master:cphBody:RequestSupportDesk", "Value=0fd91759-8c51-11d3-8aa6-0060975aec0f", ENDITEM,
				"Name=Master:cphBody:RequestAsset", "Value=", ENDITEM,
				"Name=RequestAssignment_HidValue", "Value=5AD8024A-AF7C-477D-A687-3FC5D6595C53", ENDITEM,
				"Name=RequestAssignment_HidText", "Value=Marco De Santi", ENDITEM,
				"Name=RequestDateRequired", "Value=7/6/2011", ENDITEM,
				"Name=RequestDateRequiredHidDate", "Value=7/6/2011", ENDITEM,
				"Name=Master:cphBody:RequestShortDescription", "Value=TERNA SPA_000165890_DataCenter Infrastructure_6272011", ENDITEM,
				"Name=Master:cphBody:RequestDetails", "Value=**** IT Consult **** \r\nPlease forward to DeSanti\r\n\r\nRFS-2011-25021", ENDITEM,
				"Name=Master:cphBody:RequestReason", "Value=DataCenter Infrastructure\r\n\r\n\r\nRFS-2011-25021", ENDITEM,
				"Name=Master:cphBody:RequestResolution", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestHistory", "Value=6/27/2011 4:50:44 AM\r\nCreated by: Laila Magroud\r\nInitial Type: Request for Service\r\nInitial Status: New\r\nInitial Priority: Medium\r\nInitial Assignment: Laila Magroud\r\nComment:\r\n \r\n\r\n6/27/2011 5:03:48 AM\r\nUpdated by: Laila Magroud\r\nComment:\r\n \r\n\r\n7/6/2011 8:37:13 AM\r\nStatus Changed to: In Service\r\nComment:\r\n\r\n7/6/2011 8:42:57 AM\r\nUpdated by: Changepoint Admin\r\nStatus Changed to: Pending Approval\r\nAssignment changed"" to: Marco De Santi\r\nComment:\r\n \r\n\r\n7/25/2011 9:53:38 AM\r\nUpdated by: Ali Lamine\r\nComment:\r\n \r\n\r\n", ENDITEM,
				"Name=Master:cphBody:RequestRequestUpdate", "Value=", ENDITEM,
				"Name=RequestResponsible_HidValue", "Value=", ENDITEM,
				"Name=RequestResponsible_HidText", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestRefeNumber", "Value=", ENDITEM,
				"Name=RequestParentRequest_selOriginal", "Value=", ENDITEM,
				"Name=RequestParentRequest_selReturn", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestEstEffortHrs", "Value=0.00", ENDITEM,
				"Name=Master:cphBody:RequestEstServCosts", "Value=0.0000", ENDITEM,
				"Name=Master:cphBody:RequestEstSystemCost", "Value=0.0000", ENDITEM,
				"Name=Master:cphBody:RequestEstOtherCosts", "Value=0.0000", ENDITEM,
				"Name=ResourceDemandGridtxtDate", "Value=", ENDITEM,
				"Name=RequestResource_HidValue", "Value=", ENDITEM,
				"Name=RequestResource_HidText", "Value=", ENDITEM,
				"Name=StartDate", "Value=9/27/2011", ENDITEM,
				"Name=StartDateHidDate", "Value=9/27/2011", ENDITEM,
				"Name=RequestWorkGroup_HidValue", "Value=", ENDITEM,
				"Name=RequestWorkGroup_HidText", "Value=", ENDITEM,
				"Name=EndDate", "Value=9/27/2011", ENDITEM,
				"Name=EndDateHidDate", "Value=9/27/2011", ENDITEM,
				"Name=Master:cphBody:RequestFunction", "Value=", ENDITEM,
				"Name=Master:cphBody:RequestSoftbooked", "Value=on", ENDITEM,
				"Name=Code55_TargetDropdown_HidValue", "Value=26917877-06DF-4555-9C89-54B1E147E14E", ENDITEM,
				"Name=Code55_TargetDropdown_HidText", "Value=TERNA SPA  <IT2414966 Italy (6161)>", ENDITEM,
				"Name=Code4_TargetDropdown_HidValue", "Value=F5C5E250-BB69-4E29-A885-AA6EB5E4E548", ENDITEM,
				"Name=Code4_TargetDropdown_HidText", "Value=Public", ENDITEM,
				"Name=Code5_TargetDropdown_HidValue", "Value=82ABDEF2-3927-42B6-828A-FA93F4758F6E", ENDITEM,
				"Name=Code5_TargetDropdown_HidText", "Value=Pub Direct Dev Central", ENDITEM,
				"Name=Code16_TargetDropdown_HidValue", "Value=0F9656BF-0FBF-4348-BCE1-ACE066C4C82A", ENDITEM,
				"Name=Code16_TargetDropdown_HidText", "Value=Custom", ENDITEM,
				"Name=Code38_TargetDropdown_HidValue", "Value=", ENDITEM,
				"Name=Code38_TargetDropdown_HidText", "Value=", ENDITEM,
				"Name=Code28_TargetDropdown_HidValue", "Value=E958D4CC-C84C-4955-BB88-F9730786A488", ENDITEM,
				"Name=Code28_TargetDropdown_HidText", "Value=EMEA", ENDITEM,
				"Name=Code77_TargetDropdown_HidValue", "Value=66468125-9BCE-49D8-AD6D-F66326E2B230", ENDITEM,
				"Name=Code77_TargetDropdown_HidText", "Value=Intake Manager: Post-Sales EMEA SER ICS", ENDITEM,
				"Name=Code3_TargetDropdown_HidValue", "Value=73FC3636-4B46-4CF3-9DA0-B5DFBBA12390", ENDITEM,
				"Name=Code3_TargetDropdown_HidText", "Value=ICS", ENDITEM,
				"Name=Code36_TargetDropdown_HidValue", "Value=81FE2620-8460-4069-9889-D589E0C8422B", ENDITEM,
				"Name=Code36_TargetDropdown_HidText", "Value=GO", ENDITEM,
				"Name=Code17_TargetDropdown_HidValue", "Value=8F3D8B39-7135-4E9B-BAEE-1949A68EA3F7", ENDITEM,
				"Name=Code17_TargetDropdown_HidText", "Value=Booked-in-Advance", ENDITEM,
				"Name=Text5", "Value=", ENDITEM,
				"Name=Text5HidDate", "Value=", ENDITEM,
				"Name=Text10", "Value=", ENDITEM,
				"Name=Text10HidDate", "Value=", ENDITEM,
				"Name=Code74_TargetDropdown_HidValue", "Value=52855CC4-E2F8-4814-85E1-5D05B3AE77AB", ENDITEM,
				"Name=Code74_TargetDropdown_HidText", "Value=Fixed Fee", ENDITEM,
				"Name=Code52_TargetDropdown_HidValue", "Value=", ENDITEM,
				"Name=Code52_TargetDropdown_HidText", "Value=", ENDITEM,
				"Name=Code12_TargetDropdown_HidValue", "Value=AA0E42DC-CE27-4D0C-88A5-F391F6EF586A", ENDITEM,
				"Name=Code12_TargetDropdown_HidText", "Value=No", ENDITEM,
				"Name=Code7_TargetDropdown_HidValue", "Value=AD728972-5929-4E8F-B515-C41CB20C1797", ENDITEM,
				"Name=Code7_TargetDropdown_HidText", "Value=Italy", ENDITEM,
				"Name=Text8", "Value=6/27/2011", ENDITEM,
				"Name=Text8HidDate", "Value=6/27/2011", ENDITEM,
				"Name=Code10_TargetDropdown_HidValue", "Value=", ENDITEM,
				"Name=Code10_TargetDropdown_HidText", "Value=", ENDITEM,
				"Name=Text9", "Value=6/27/2011", ENDITEM,
				"Name=Text9HidDate", "Value=6/27/2011", ENDITEM,
				"Name=Code11_TargetDropdown_HidValue", "Value=", ENDITEM,
				"Name=Code11_TargetDropdown_HidText", "Value=", ENDITEM,
				"Name=Code27_TargetDropdown_HidValue", "Value=5AD8024A-AF7C-477D-A687-3FC5D6595C53", ENDITEM,
				"Name=Code27_TargetDropdown_HidText", "Value=Marco De Santi", ENDITEM,
				"Name=Code15_TargetDropdown_HidValue", "Value=D99C5531-6320-42B8-885A-01C7CFC9EA5B", ENDITEM,
				"Name=Code15_TargetDropdown_HidText", "Value=Default Project - Solutioning", ENDITEM,
				"Name=Code1_TargetDropdown_HidValue", "Value=5394F163-67DA-4F67-AD5A-EC824665DCE2", ENDITEM,
				"Name=Code1_TargetDropdown_HidText", "Value=Paul Sisung", ENDITEM,
				"Name=Code39_TargetDropdown_HidValue", "Value=", ENDITEM,
				"Name=Code39_TargetDropdown_HidText", "Value=", ENDITEM,
				"Name=hidWFResourceID", "Value={{rid}}", ENDITEM,
				"Name=hidWFFilters", "Value=Code16,Code28,Code3,Code30,Code45,Code47,Status,Type", ENDITEM,
				"Name=hidWFFiltersReset", "Value=True,True,True,True,True,True,True,True", ENDITEM,
				"Name=hidWFFields", "Value=Status", ENDITEM,
				"Name=hidEntity", "Value=Request", ENDITEM,
				"Name=hidWFEntityID", "Value={{entityid}}", ENDITEM,
				"Name=hidWFStateDefID", "Value=258c52b1-8354-43ae-82fd-434d206e3645", ENDITEM,
				"Name=hidWFFieldValues", "Value=<root><workflowfilter><entity>Request</entity><entityid>{{entityid}}</entityid><statedefid>258c52b1-8354-43ae-82fd-434d206e3645</statedefid><field>Status</field><value></value><orgvalue>PEN</orgvalue><orgstate>258c52b1-8354-43ae-82fd-434d206e3645</orgstate><reset>False</reset><transitionid></transitionid></workflowfilter></root>", ENDITEM,
				"Name=hidCurrentValue", "Value=PEN", ENDITEM,
				"Name=hidDesireValue", "Value=APP", ENDITEM,
				"Name=hidOrgStateDef", "Value=258c52b1-8354-43ae-82fd-434d206e3645", ENDITEM,
				"Name=hidisNet", "Value=true", ENDITEM,
				"Name=hidWFFilterValues", "Value=", ENDITEM,
				"Name=hidInitialState", "Value=", ENDITEM,
				"Name=hidisProfile", "Value=1", ENDITEM,
				"Name=hidProcess", "Value=undefined", ENDITEM,
				"Name=Master:cphBody:hidSubmitAction", "Value=save", ENDITEM,
				"Name=Master:cphBody:hidRequestId", "Value={{entityid}}", ENDITEM,
				"Name=Master:cphBody:hidNewRequest", "Value=no", ENDITEM,
				"Name=Master:cphBody:hidInitiator", "Value=D88620E1-2B6D-4851-AD9B-77864D16CC4C", ENDITEM,
				"Name=Master:cphBody:hidCustomer", "Value=06CF3214-364A-4BCE-99CD-525C58A88062", ENDITEM,
				"Name=Master:cphBody:hidEngagement", "Value=020ea6aa-938f-40f4-9a74-4d611bb0fbae", ENDITEM,
				"Name=Master:cphBody:hidRequestType", "Value=RFS", ENDITEM,
				"Name=Master:cphBody:hidRequestStatus", "Value=PEN", ENDITEM,
				"Name=Master:cphBody:hidResourceOrQueue", "Value=ASS", ENDITEM,
				"Name=Master:cphBody:hidProduct", "Value=03991a24-2791-4664-b11f-930238082209", ENDITEM,
				"Name=Master:cphBody:hidPriority", "Value=0a043812-0e3f-4576-b80a-b9fbb1fe8dfd", ENDITEM,
				"Name=Master:cphBody:hidCategory", "Value=", ENDITEM,
				"Name=Master:cphBody:hidSubCategory", "Value=", ENDITEM,
				"Name=Master:cphBody:hidAsset", "Value=", ENDITEM,
				"Name=Master:cphBody:hidBillingType", "Value=", ENDITEM,
				"Name=Master:cphBody:hidClientTimezoneOffset", "Value=5", ENDITEM,
				"Name=Master:cphBody:hidRequestDemandId", "Value=", ENDITEM,
				"Name=Master:cphBody:hidRequestDemandsValues", "Value=", ENDITEM,
				"Name=Master:cphBody:hidRowNumberForFS", "Value=", ENDITEM,
				"Name=Master:cphBody:hidAttach", "Value=noattach", ENDITEM,
				"Name=Master:cphBody:hidCarsParam", "Value=", ENDITEM,
				"Name=Master:cphBody:hidCarsPXMLId", "Value=", ENDITEM,
				"Name=Master:cphBody:hidCarsURL", "Value=", ENDITEM,
				"Name=Master:cphBody:hidTime", "Value=9/27/2011 5:44:06 AM", ENDITEM,
				"Name=Master:cphBody:hidOpenTime", "Value=9/27/2011 5:44:07 AM", ENDITEM,
				"Name=Master:cphBody:hidDescMappingUDF", "Value=", ENDITEM,
				"Name=Master:cphBody:hidCopyTemplateId", "Value=", ENDITEM,
				"Name=Master:cphBody:hidbCopied", "Value=False", ENDITEM,
				"Name=Master:cphBody:hidSupportDeskId", "Value=0fd91759-8c51-11d3-8aa6-0060975aec0f", ENDITEM,
				"Name=aster:cphBody:hidSupportDeskName", "Value=Order Management", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={eventvalidation_form15}", ENDITEM,
				LAST);
		lr_end_sub_transaction("dcRequest_aspx_2", LR_AUTO);


		lr_start_sub_transaction("RecordLock_aspx_2", "OTB_SaveAssignDeliveryPM");
			web_custom_request("RecordLock.aspx_2",
				"URL=http://{server_server}/Core/Locking/RecordLock.aspx?rId={{rid}}&sno={sno}&ui=W&LockAction=RELEASELOCK&RecordType=REQ&RecordId={{entityid}}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("RecordLock_aspx_2", LR_AUTO);


		lr_start_sub_transaction("cookie", "OTB_SaveAssignDeliveryPM");
			web_custom_request("cookie",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/helpdesk/dcRequest.aspx?rid={{rid}}&sno={sno}&ui=W&reqId={{entityid}}&action=edit&layout=TABBED",
				"Mode=HTTP",
				"Body={\"Rid\": \"{{rid}}\",\"objid\": \"masterpagetabbed\",\"objid2\": \"request\",\"xmlstr\": \"%3croot%3e%3ccontentsection1%3e0%3c/contentsection1%3e%3ccontentsection8%3e1%3c/contentsection8%3e%3c/root%3e%0d%0a\"}",
LAST);
		lr_end_sub_transaction("cookie", LR_AUTO);


	lr_end_transaction("OTB_SaveAssignDeliveryPM",LR_AUTO);


lr_start_transaction("Home_aspx_2");
web_url("Home.aspx_2",
"URL=http://{server_server}/core/ui/Home.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
LAST);


lr_end_transaction("Home_aspx_2",LR_AUTO);
lr_think_time(1);
lr_start_transaction("uiHomePage_aspx_3");
web_custom_request("uiHomePage.aspx_3",
"URL=http://{server_server}/core/ui/uiHomePage.aspx?rId={{rid}}&sno={sno}&ui=W&portalTemplateId=80e812a4-05ac-4d57-8eee-93ebbe284fad&isUserDefined=True",
"Method=POST",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/Home.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
LAST);


lr_end_transaction("uiHomePage_aspx_3",LR_AUTO);
lr_think_time(1);
lr_start_transaction("uiHomePageData_aspx_3");
web_url("uiHomePageData.aspx_3",
"URL=http://{server_server}/core/ui/uiHomePageData.aspx?rId={{rid}}&sno={sno}&ui=W&Action=GetCustomLayout&ResourceId={{rid}}&PortalTemplateId=80e812a4-05ac-4d57-8eee-93ebbe284fad",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiHomePage.aspx?rId={{rid}}&sno={sno}&ui=W&portalTemplateId=80e812a4-05ac-4d57-8eee-93ebbe284fad&isUserDefined=True",
				"Mode=HTTP",
LAST);


lr_end_transaction("uiHomePageData_aspx_3",LR_AUTO);
lr_think_time(1);
lr_start_transaction("dshReminder_asp_2");
web_url("dshReminder.asp_2",
"URL=http://{server_server}/core/ui/dshReminder.asp?rid={{rid}}&sno={sno}&ui=W&home=Yes",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiHomePage.aspx?rId={{rid}}&sno={sno}&ui=W&portalTemplateId=80e812a4-05ac-4d57-8eee-93ebbe284fad&isUserDefined=True",
				"Mode=HTTP",
LAST);


lr_end_transaction("dshReminder_asp_2",LR_AUTO);
lr_think_time(1);
lr_start_transaction("rpWorkflow_aspx_2");
web_url("rpWorkflow.aspx_2",
"URL=http://{server_server}/core/ui/rpWorkflow.aspx?rid={{rid}}&sno={sno}&ui=W&entity=REQUEST",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
LAST);


lr_end_transaction("rpWorkflow_aspx_2",LR_AUTO);
lr_think_time(1);
lr_start_transaction("ReportScheduleDataSrv_aspx");
web_custom_request("ReportScheduleDataSrv.aspx",
"URL=http://{server_server}/ReportExecEngine/ReportSchedule/ReportScheduleDataSrv.aspx?Action=12&rId={{rid}}&ui=W&sno={sno}&login=NT&cars=false",
"Method=POST",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiBrowser.aspx?rId={{rid}}&ui=W&sno={sno}&login=NT&cars=false",
				"Mode=HTTP",
LAST);


lr_end_transaction("ReportScheduleDataSrv_aspx",LR_AUTO);
lr_think_time(1);
	lr_think_time(15);


	lr_start_transaction("OOTB_OpenAssignDeliveryPM");
		lr_start_sub_transaction("ApproveStep_aspx_3", "OOTB_OpenAssignDeliveryPM");
			web_url("ApproveStep.aspx_3",
				"URL=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={processstepinstanceid}&StepType=COM&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId={entityid}",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx_3", LR_AUTO);


	lr_end_transaction("OOTB_OpenAssignDeliveryPM",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_AssignDeliveryPM_PostSales");
		lr_start_sub_transaction("ApproveStep_aspx_4", "OOTB_AssignDeliveryPM_PostSales");
			web_submit_data("ApproveStep.aspx_4",
				"Action=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=COM&ProcessId={{processid}}&Entity=REQUEST&ProcessStepId={{processstepid}}&EntityId={{entityid}}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=COM&ProcessId={{processid}}&Entity=REQUEST&ProcessStepId={{processstepid}}&EntityId={{entityid}}",
				"Mode=HTTP",
				ITEMDATA,
				"Name=VIEWSTATE", "Value={viewstate_form18}", ENDITEM,
				"Name=EVENTVALIDATION", "Value={eventvalidation_form19}", ENDITEM,
				"Name=TextInstruction", "Value=Mark status as COMPLETED once:\r\n1.  Delivery PM name selected and entered into the   RFS\r\n2. Project Template selected and entered into the RFS", ENDITEM,
				"Name=DpdSta", "Value=COM|1|0", ENDITEM,
				"Name=TextCom", "Value=", ENDITEM,
				"Name=inputResourceWF", "Value=", ENDITEM,
				"Name=s_hid_Action", "Value=S", ENDITEM,
				"Name=s_hid_Status", "Value=COM", ENDITEM,
				"Name=s_hid_Complete", "Value=1", ENDITEM,
				"Name=s_hid_Reassign", "Value=0", ENDITEM,
				"Name=hidAssignResid", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx_4", LR_AUTO);


	lr_end_transaction("OOTB_AssignDeliveryPM_PostSales",LR_AUTO);


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


	lr_end_transaction("uc_ootb_wf_assigndelivery",LR_AUTO);

	return 0;
}
