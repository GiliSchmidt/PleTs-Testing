Action()
{
web_set_max_html_param_len("9999999");
lr_start_transaction("uc_ootb_wf_gatereview0");


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
"Referer=http://{server_server}/core/ui/dshReminder.asp?rid={{rid}}&sno={sno}&ui=W&home=Yes",
				"Mode=HTTP",
LAST);


lr_end_transaction("dshReminder_asp",LR_AUTO);
lr_think_time(1);
lr_start_transaction("rpWorkflow_aspx");
web_url("rpWorkflow.aspx",
"URL=http://{server_server}/core/ui/rpWorkflow.aspx?rid={{rid}}&sno={sno}&ui=W&entity=REQUEST",
"Resource=0",
"RecContentType=text/html",
"Referer=http://{server_server}/core/ui/uiDrawFrames.asp?rid={{rid}}&sno={sno}&ui=W&home=Yes&CPC=1&CB=1&CRQ=1&MPC=1&CKM=1&VTS=1&VEX=1&TA=1&EA=1&VCC=1&VE=1&VPR=1&ME=1&OFB=1&MTK=1",
				"Mode=HTTP",
LAST);


lr_end_transaction("rpWorkflow_aspx",LR_AUTO);
lr_think_time(15);
	lr_think_time(15);


	lr_start_transaction("OOTB_OpenGateReview0%22");
		lr_start_sub_transaction("ApproveStep_aspx", "OOTB_OpenGateReview0%22");
			web_url("ApproveStep.aspx",
				"URL=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId={entityid}",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx", LR_AUTO);


	lr_end_transaction("OOTB_OpenGateReview0%22",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("OOTB_GateReview0_SolutionArchitect");
		lr_start_sub_transaction("ApproveStep_aspx_2", "OOTB_GateReview0_SolutionArchitect");
			web_submit_data("ApproveStep.aspx_2",
				"Action=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={{processstepinstanceid}}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId={entityid}",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/WorkflowManagement/Action/ApproveStep.aspx?rId={{rid}}&sno={sno}&ui=W&StepInstanceId={processstepinstanceid}&StepType=APP&ProcessId={processid}&Entity=REQUEST&ProcessStepId={processstepid}&EntityId={entityid}",
				"Mode=HTTP",
				ITEMDATA,
				"Name=__VIEWSTATE", "Value={viewstate_form12}", ENDITEM,
				"Name=__EVENTVALIDATION", "Value={eventvalidation_form13}", ENDITEM,
				"Name=TextInstruction", "Value=Mark status as APPROVED if:\r\n1.  RFS is set-up correctly (e.g., look in Salesforce.com to confirm full customer information)\r\n2.  You are correct SA to solution this project\r\n3.  If SA is needed from another tower as well, you have emailed Project Intake to request that they submit another Service Request to engage them\r\n\r\nMark status as REJECTED if the RFS information is inaccurate or incomplete OR if this RFS needs to be assigned to another Solution", ENDITEM,
				"Name=Architect. Enter relevant notes in the Comments field.", "Value=", ENDITEM,
				"Name=DpdSta", "Value=APP|1|0", ENDITEM,
				"Name=TextCom", "Value=", ENDITEM,
				"Name=inputResourceWF", "Value=", ENDITEM,
				"Name=s_hid_Action", "Value=S", ENDITEM,
				"Name=s_hid_Status", "Value=APP", ENDITEM,
				"Name=s_hid_Complete", "Value=1", ENDITEM,
				"Name=s_hid_Reassign", "Value=0", ENDITEM,
				"Name=hidAssignResid", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("ApproveStep_aspx_2", LR_AUTO);


	lr_end_transaction("OOTB_GateReview0_SolutionArchitect",LR_AUTO);


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


	lr_end_transaction("uc_ootb_wf_gatereview0",LR_AUTO);

	return 0;
}
