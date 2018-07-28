Action()
{
web_set_max_html_param_len("9999999");
lr_start_transaction("uc_ootb_wf_customerapproval");


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
	lr_think_time(1);


	lr_start_transaction("OOTB_OpenCustomerApproval");
		lr_start_sub_transaction("ApproveStep_aspx_9", "OOTB_OpenCustomerApproval");
			web_url("ApproveStep.aspx_9",
				"URL=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId={entityid}",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx_9", LR_AUTO);


	lr_end_transaction("OOTB_OpenCustomerApproval",LR_AUTO);


lr_start_transaction("ReportScheduleDataSrv_aspx_3");
web_custom_request("ReportScheduleDataSrv.aspx_3",
"URL=http://{server_server}/ReportExecEngine/ReportSchedule/ReportScheduleDataSrv.aspx?Action=12&rId={{rid}}&ui=W&sno={sno}&login=NT&cars=false",
"Method=POST",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiBrowser.aspx?rId={{rid}}&ui=W&sno={sno}&login=NT&cars=false",
				"Mode=HTTP",
LAST);


lr_end_transaction("ReportScheduleDataSrv_aspx_3",LR_AUTO);
lr_think_time(1);
	lr_think_time(1);


	lr_start_transaction("OOTB_CustomerApproval_SolutionArchitect");
		lr_start_sub_transaction("ApproveStep_aspx_10", "OOTB_CustomerApproval_SolutionArchitect");
			web_submit_data("ApproveStep.aspx_10",
				"Action=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId=5256ec30-e858-47d6-982d-69a98c797bec",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId=5256ec30-e858-47d6-982d-69a98c797bec",
				"Mode=HTTP",
				ITEMDATA,
				"Name=__VIEWSTATE", "Value={viewstate_form12}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={eventvalidation_form13}", ENDITEM,
				"Name=TextInstruction", "Value=Mark status as COMPLETED once:\r\n1.  Project Status Reporting delivered and uploaded\r\n2.  Project Plan followed\r\n3.  Communication, Change Management, Quality Plan followed\r\n4.  Risk / Issue Log updated\r\n5.  Project deliverables provided to customer, accepted, and uploaded (if applicable)\r\n6.  New orders associated to the Engagement\r\n\r\nIf Exceptions exist, note these in the Project \"Gate Review 4 Exceptions Comments\" field.", ENDITEM,
				"Name=DpdSta", "Value=APP|1|0", ENDITEM,
				"Name=TextCom", "Value=", ENDITEM,
				"Name=inputResourceWF", "Value=", ENDITEM,
				"Name=s_hid_Action", "Value=S", ENDITEM,
				"Name=s_hid_Status", "Value=APP", ENDITEM,
				"Name=s_hid_Complete", "Value=1", ENDITEM,
				"Name=s_hid_Reassign", "Value=0", ENDITEM,
				"Name=hidAssignResid", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx_10", LR_AUTO);


		lr_start_sub_transaction("rpWorkflow_aspx_8", "OOTB_CustomerApproval_SolutionArchitect");
			web_url("rpWorkflow.aspx_8",
				"URL=http://{server_server}/core/ui/rpWorkflow.aspx?ui=W&sno={sno}&rid={{rid}}&entity=REQUEST",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("rpWorkflow_aspx_8", LR_AUTO);


	lr_end_transaction("OOTB_CustomerApproval_SolutionArchitect",LR_AUTO);


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


	lr_end_transaction("uc_ootb_wf_customerapproval",LR_AUTO);

	return 0;
}
