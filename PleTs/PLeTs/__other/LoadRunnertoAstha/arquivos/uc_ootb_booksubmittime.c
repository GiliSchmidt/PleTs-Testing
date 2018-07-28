Action()
{
web_set_max_html_param_len("9999999");
lr_start_transaction("uc_ootb_booksubmittime");


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


	lr_start_transaction("OOTB_OpenTimeSheet");
		lr_start_sub_transaction("UtilityPostXml_aspx", "OOTB_OpenTimeSheet");
			web_custom_request("UtilityPostXml.aspx",
				"URL=http://{server_server}/Utility/UtilityPostXml.aspx?rid={{rid}}&sno={{sno}}&ui=W&Action=0",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/UI/uiDirectory.aspx?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				"Body=VTS",
LAST);
		lr_end_sub_transaction("UtilityPostXml_aspx", LR_AUTO);


		lr_start_sub_transaction("tvTimeTree_asp", "OOTB_OpenTimeSheet");
			web_url("tvTimeTree.asp",
				"URL=http://{server_server}/Core/Treeviews/tvTimeTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={{sno}}&ui=W&home=Yes",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvTimeTree_asp", LR_AUTO);


		lr_start_sub_transaction("TimeSheetTreeStore", "OOTB_OpenTimeSheet");
			web_url("TimeSheetTreeStore",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/TimeSheetTreeStore",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimeTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("TimeSheetTreeStore", LR_AUTO);


		lr_start_sub_transaction("vwTimeSheet_asp", "OOTB_OpenTimeSheet");
			web_url("vwTimeSheet.asp",
				"URL=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/ui/uiDrawFrames.aspx?rid={{rid}}&sno={{sno}}&ui=W&home=Yes",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("vwTimeSheet_asp", LR_AUTO);


	web_concurrent_start(NULL);


		lr_start_sub_transaction("frmTimeSheet_Status_asp", "OOTB_OpenTimeSheet");
			web_url("frmTimeSheet_Status.asp",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet_Status.asp?rid={{rid}}&reId=&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_Status_asp", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp", "OOTB_OpenTimeSheet");
			web_url("frmTimeSheet.asp",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId=&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp", LR_AUTO);


		lr_start_sub_transaction("popCalJQ_TimesheetStatus_asp", "OOTB_OpenTimeSheet");
			web_url("popCalJQ_TimesheetStatus.asp",
				"URL=http://{server_server}/Projects/popCalJQ_TimesheetStatus.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("popCalJQ_TimesheetStatus_asp", LR_AUTO);


		lr_start_sub_transaction("popCalJQ_asp", "OOTB_OpenTimeSheet");
			web_url("popCalJQ.asp",
				"URL=http://{server_server}/Projects/popCalJQ.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("popCalJQ_asp", LR_AUTO);


	web_concurrent_end(NULL);


		lr_start_sub_transaction("vwTimeSheet", "OOTB_OpenTimeSheet");
			web_url("vwTimeSheet",
				"URL=http://{server_server}/DataServices/ServerCookieService.svc/cookie/{rid}/vwTimeSheet",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("vwTimeSheet", LR_AUTO);


	lr_end_transaction("OOTB_OpenTimeSheet",LR_AUTO);


	lr_think_time(1);


	lr_start_transaction("OOTB_FilterTimesheetProject");
		lr_start_sub_transaction("tvTimesheet_aspx", "OOTB_FilterTimesheetProject");
			web_url("tvTimesheet.aspx",
				"URL=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimeTree.asp?rid={{rid}}&sno={{sno}}&ui=W",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("tvTimesheet_aspx", LR_AUTO);


	lr_end_transaction("OOTB_FilterTimesheetProject",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_ExpandTimesheetTree");
		lr_start_sub_transaction("ifrtvTimeSheet_aspx_5", "OOTB_ExpandTimesheetTree");
			web_url("ifrtvTimeSheet.aspx_5",
				"URL=http://{server_server}/Core/Treeviews/ifrtvTimeSheet.aspx?cid=tvEngTime&rid={{rid}}&reId=&sno={{sno}}&ui=W&searchFor=Perf&LoadFor=0&cptype=OpenTask&id=OpenTask&FirstLoad=1&view=PTC&render=alphabetic&showwbs=0",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvTimeSheet_aspx_5", LR_AUTO);


		lr_start_sub_transaction("ifrtvTimeSheet_aspx_6", "OOTB_ExpandTimesheetTree");
			web_url("ifrtvTimeSheet.aspx_6",
				"URL=http://{server_server}/Core/Treeviews/ifrtvTimeSheet.aspx?cid=tvEngTime&rid={{rid}}&reId=&sno={{sno}}&ui=W&searchFor=Perf&LoadFor=0&cptype=OpenTime&id=OpenTime&FirstLoad=1&view=PTC&render=alphabetic&showwbs=0",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvTimeSheet_aspx_6", LR_AUTO);


		lr_start_sub_transaction("ifrtvTimeSheet_aspx_7", "OOTB_ExpandTimesheetTree");
			web_url("ifrtvTimeSheet.aspx_7",
				"URL= http://{server_server}/Core/Treeviews/ifrtvTimeSheet.aspx?cid=tvEngTime&rid={{rid}}&reId=&sno={{sno}}&ui=W&searchFor=Perf&LoadFor=0&cptype=cus_u_ex&id={taskid_taskid}&FirstLoad=1&view=PTC&render=alphabetic&showwbs=0",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvTimeSheet_aspx_7", LR_AUTO);


		lr_start_sub_transaction("ifrtvTimeSheet_aspx_8", "OOTB_ExpandTimesheetTree");
			web_url("ifrtvTimeSheet.aspx_8",
				"URL=http://{server_server}/Core/Treeviews/ifrtvTimeSheet.aspx?cid=tvEngTime&rid={{rid}}&reId=&sno={{sno}}&ui=W&searchFor=Perf&LoadFor=0&cptype=eng_u_ex&id={taskid_taskid}&FirstLoad=1&view=PTC&render=alphabetic&showwbs=0 ",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvTimeSheet_aspx_8", LR_AUTO);


		lr_start_sub_transaction("ifrtvTimeSheet_aspx_9", "OOTB_ExpandTimesheetTree");
			web_url("ifrtvTimeSheet.aspx_9",
				"URL=http://{server_server}/Core/Treeviews/ifrtvTimeSheet.aspx?cid=tvEngTime&rid={{rid}}&reId=&sno={{sno}}&ui=W&searchFor=Perf&LoadFor=0&cptype=proj_u_ex&id={taskid_taskid}&FirstLoad=1&view=PTC&render=alphabetic&showwbs=0",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=alphabetic&showwbs=0&srchFor=Perf",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("ifrtvTimeSheet_aspx_9", LR_AUTO);


	lr_end_transaction("OOTB_ExpandTimesheetTree",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_DragTimesheetTask");
		lr_start_sub_transaction("LoadTimeSheetData_aspx", "OOTB_DragTimesheetTask");
			web_url("LoadTimeSheetData.aspx",
				"URL=http://{server_server}/Core/Treeviews/LoadTimeSheetData.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&LoadFor=&cptype=opentask_en&id={{taskid_taskid}}",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Treeviews/tvTimesheet.aspx?rid={{rid}}&sno={{sno}}&ui=W&reid=&view=PTC&render=wbs&showwbs=0&srchFor=esp",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("LoadTimeSheetData_aspx", LR_AUTO);


	lr_end_transaction("OOTB_DragTimesheetTask",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_PostTimesheetHours");
		lr_start_sub_transaction("postClosePeriod_asp", "OOTB_PostTimesheetHours");
			web_custom_request("postClosePeriod.asp",
				"URL=http://{server_server}/Finance/ClosePeriod/postClosePeriod.asp?rId={{rid}}&sno={{sno}}&ui=W&Type=T&PPP=0&reId=&boid={83989a39-81fd-43e3-8e97-a93a4e53a641}&date=7/30/2012",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("postClosePeriod_asp", LR_AUTO);


		lr_start_sub_transaction("collapse_b_gif_2", "OOTB_PostTimesheetHours");
			web_url("collapse_b.gif_2",
				"URL=http://{server_server}/images/UI/collapse_b.gif",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("collapse_b_gif_2", LR_AUTO);


		lr_start_sub_transaction("dsBookTime_asp", "OOTB_PostTimesheetHours");
			web_submit_data("dsBookTime.asp",
				"Action=http://{server_server}/core/time_Sheet/dsBookTime.asp?rid={{rid}}&reId=&sno={{sno}}&ui=%20W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId=&sno={{sno}}&ui=W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=hidDescription", "Value=", ENDITEM,
				"Name=hidCurDate", "Value=7/30/2012", ENDITEM,
				"Name=hidRegularHours", "Value=8.00", ENDITEM,
				"Name=hidOverTimeHours", "Value=0.00", ENDITEM,
				"Name=hidCode1", "Value=", ENDITEM,
				"Name=hidCode2", "Value=", ENDITEM,
				"Name=hidCode3", "Value=", ENDITEM,
				"Name=hidGroupId", "Value={ec2353d1-3b9b-4075-bab5-fbb84529bacc}", ENDITEM,
				"Name=hidLocationId", "Value={949662a4-f4c3-403b-a293-8032e5e909ab}", ENDITEM,
				"Name=hidCategoryId", "Value={aa9c83c1-6e1e-4974-9dec-7c554cc429d2}", ENDITEM,
				"Name=hidCodeId", "Value={e716cde8-72a0-481d-9d29-99fbd74a9afa}", ENDITEM,
				"Name=hidTimeId", "Value=", ENDITEM,
				"Name=hidTaskId", "Value={{taskid_taskid}}", ENDITEM,
				"Name=hidTimeZone", "Value=", ENDITEM,
				"Name=hidTimeType", "Value=t", ENDITEM,
				"Name=hidAdjustedRegular", "Value=", ENDITEM,
				"Name=hidAdjustedOverTime", "Value=", ENDITEM,
				"Name=hidXCell", "Value=7", ENDITEM,
				"Name=hidYCell", "Value=2", ENDITEM,
				"Name=hidStartHour", "Value=", ENDITEM,
				"Name=hidStartMinute", "Value=", ENDITEM,
				"Name=hidPMAM", "Value=", ENDITEM,
				"Name=hidAction", "Value=add", ENDITEM,
				"Name=hidAdjustAction", "Value=", ENDITEM,
				"Name=hidAudit", "Value=0", ENDITEM,
				"Name=hidComment", "Value=", ENDITEM,
				"Name=hidbAdjustedTime", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("dsBookTime_asp", LR_AUTO);


		lr_start_sub_transaction("postClosePeriod_asp_2", "OOTB_PostTimesheetHours");
			web_custom_request("postClosePeriod.asp_2",
				"URL=http://{server_server}/Finance/ClosePeriod/postClosePeriod.asp?rId={{rid}}&sno={{sno}}&ui=W&Type=T&PPP=0&reId=&boid={83989a39-81fd-43e3-8e97-a93a4e53a641}&date=7/31/2012",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("postClosePeriod_asp_2", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp_2", "OOTB_PostTimesheetHours");
			web_url("frmTimeSheet.asp_2",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp_2", LR_AUTO);


		lr_start_sub_transaction("postClosePeriod_asp_3", "OOTB_PostTimesheetHours");
			web_custom_request("postClosePeriod.asp_3",
				"URL=http://{server_server}/Finance/ClosePeriod/postClosePeriod.asp?rId={{rid}}&sno={{sno}}&ui=W&Type=T&PPP=0&reId=&boid={83989a39-81fd-43e3-8e97-a93a4e53a641}&date=8/1/2012",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("postClosePeriod_asp_3", LR_AUTO);


		lr_start_sub_transaction("dsBookTime_asp_2", "OOTB_PostTimesheetHours");
			web_submit_data("dsBookTime.asp_2",
				"Action=http://{server_server}/core/time_Sheet/dsBookTime.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=  W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=hidDescription", "Value=", ENDITEM,
				"Name=hidCurDate", "Value=7/31/2012", ENDITEM,
				"Name=hidRegularHours", "Value=8.00", ENDITEM,
				"Name=hidOverTimeHours", "Value=0.00", ENDITEM,
				"Name=hidCode1", "Value=", ENDITEM,
				"Name=hidCode2", "Value=", ENDITEM,
				"Name=hidCode3", "Value=", ENDITEM,
				"Name=hidGroupId", "Value={ec2353d1-3b9b-4075-bab5-fbb84529bacc}", ENDITEM,
				"Name=hidLocationId", "Value={949662a4-f4c3-403b-a293-8032e5e909ab}", ENDITEM,
				"Name=hidCategoryId", "Value={aa9c83c1-6e1e-4974-9dec-7c554cc429d2}", ENDITEM,
				"Name=hidCodeId", "Value={e716cde8-72a0-481d-9d29-99fbd74a9afa}", ENDITEM,
				"Name=hidTimeId", "Value=", ENDITEM,
				"Name=hidTaskId", "Value={{taskid_taskid}}", ENDITEM,
				"Name=hidTimeZone", "Value=", ENDITEM,
				"Name=hidTimeType", "Value=t", ENDITEM,
				"Name=hidAdjustedRegular", "Value=", ENDITEM,
				"Name=hidAdjustedOverTime", "Value=", ENDITEM,
				"Name=hidXCell", "Value=8", ENDITEM,
				"Name=hidYCell", "Value=2", ENDITEM,
				"Name=hidStartHour", "Value=", ENDITEM,
				"Name=hidStartMinute", "Value=", ENDITEM,
				"Name=hidPMAM", "Value=", ENDITEM,
				"Name=hidAction", "Value=add", ENDITEM,
				"Name=hidAdjustAction", "Value=", ENDITEM,
				"Name=hidAudit", "Value=0", ENDITEM,
				"Name=hidComment", "Value=", ENDITEM,
				"Name=hidbAdjustedTime", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("dsBookTime_asp_2", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp_3", "OOTB_PostTimesheetHours");
			web_url("frmTimeSheet.asp_3",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp_3", LR_AUTO);


		lr_start_sub_transaction("dsBookTime_asp_3", "OOTB_PostTimesheetHours");
			web_submit_data("dsBookTime.asp_3",
				"Action=http://{server_server}/core/time_Sheet/dsBookTime.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=    W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=hidDescription", "Value=", ENDITEM,
				"Name=hidCurDate", "Value=8/1/2012", ENDITEM,
				"Name=hidRegularHours", "Value=8.00", ENDITEM,
				"Name=hidOverTimeHours", "Value=0.00", ENDITEM,
				"Name=hidCode1", "Value=", ENDITEM,
				"Name=hidCode2", "Value=", ENDITEM,
				"Name=hidCode3", "Value=", ENDITEM,
				"Name=hidGroupId", "Value={ec2353d1-3b9b-4075-bab5-fbb84529bacc}", ENDITEM,
				"Name=hidLocationId", "Value={949662a4-f4c3-403b-a293-8032e5e909ab}", ENDITEM,
				"Name=hidCategoryId", "Value={aa9c83c1-6e1e-4974-9dec-7c554cc429d2}", ENDITEM,
				"Name=hidCodeId", "Value={e716cde8-72a0-481d-9d29-99fbd74a9afa}", ENDITEM,
				"Name=hidTimeId", "Value=", ENDITEM,
				"Name=hidTaskId", "Value={{taskid_taskid}}", ENDITEM,
				"Name=hidTimeZone", "Value=", ENDITEM,
				"Name=hidTimeType", "Value=t", ENDITEM,
				"Name=hidAdjustedRegular", "Value=", ENDITEM,
				"Name=hidAdjustedOverTime", "Value=", ENDITEM,
				"Name=hidXCell", "Value=9", ENDITEM,
				"Name=hidYCell", "Value=2", ENDITEM,
				"Name=hidStartHour", "Value=", ENDITEM,
				"Name=hidStartMinute", "Value=", ENDITEM,
				"Name=hidPMAM", "Value=", ENDITEM,
				"Name=hidAction", "Value=", ENDITEM,
				"Name=hidAdjustAction", "Value=", ENDITEM,
				"Name=hidAudit", "Value=0", ENDITEM,
				"Name=hidComment", "Value=", ENDITEM,
				"Name=hidbAdjustedTime", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("dsBookTime_asp_3", LR_AUTO);


		lr_start_sub_transaction("postClosePeriod_asp_4", "OOTB_PostTimesheetHours");
			web_custom_request("postClosePeriod.asp_4",
				"URL=http://{server_server}/Finance/ClosePeriod/postClosePeriod.asp?rId={{rid}}&sno={{sno}}&ui=W&Type=T&PPP=0&reId=&boid={83989a39-81fd-43e3-8e97-a93a4e53a641}&date=8/2/2012",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=hhttp://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("postClosePeriod_asp_4", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp_4", "OOTB_PostTimesheetHours");
			web_url("frmTimeSheet.asp_4",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20%20W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp_4", LR_AUTO);


		lr_start_sub_transaction("dsBookTime_asp_4", "OOTB_PostTimesheetHours");
			web_submit_data("dsBookTime.asp_4",
				"Action=http://{server_server}/core/time_Sheet/dsBookTime.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20%20%20W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=      W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=hidDescription", "Value=", ENDITEM,
				"Name=hidCurDate", "Value=8/2/2012", ENDITEM,
				"Name=hidRegularHours", "Value=8.00", ENDITEM,
				"Name=hidOverTimeHours", "Value=0.00", ENDITEM,
				"Name=hidCode1", "Value=", ENDITEM,
				"Name=hidCode2", "Value=", ENDITEM,
				"Name=hidCode3", "Value=", ENDITEM,
				"Name=hidGroupId", "Value={ec2353d1-3b9b-4075-bab5-fbb84529bacc}", ENDITEM,
				"Name=hidLocationId", "Value={949662a4-f4c3-403b-a293-8032e5e909ab}", ENDITEM,
				"Name=hidCategoryId", "Value={aa9c83c1-6e1e-4974-9dec-7c554cc429d2}", ENDITEM,
				"Name=hidCodeId", "Value={e716cde8-72a0-481d-9d29-99fbd74a9afa}", ENDITEM,
				"Name=hidTimeId", "Value=", ENDITEM,
				"Name=hidTaskId", "Value={{taskid_taskid}}", ENDITEM,
				"Name=hidTimeZone", "Value=", ENDITEM,
				"Name=hidTimeType", "Value=t", ENDITEM,
				"Name=hidAdjustedRegular", "Value=", ENDITEM,
				"Name=hidAdjustedOverTime", "Value=", ENDITEM,
				"Name=hidXCell", "Value=10", ENDITEM,
				"Name=hidYCell", "Value=2", ENDITEM,
				"Name=hidStartHour", "Value=", ENDITEM,
				"Name=hidStartMinute", "Value=", ENDITEM,
				"Name=hidPMAM", "Value=", ENDITEM,
				"Name=hidAction", "Value=add", ENDITEM,
				"Name=hidAdjustAction", "Value=", ENDITEM,
				"Name=hidAudit", "Value=0", ENDITEM,
				"Name=hidComment", "Value=", ENDITEM,
				"Name=hidbAdjustedTime", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("dsBookTime_asp_4", LR_AUTO);


		lr_start_sub_transaction("postClosePeriod_asp_5", "OOTB_PostTimesheetHours");
			web_custom_request("postClosePeriod.asp_5",
				"URL=http://{server_server}/Finance/ClosePeriod/postClosePeriod.asp?rId={{rid}}&sno={{sno}}&ui=W&Type=T&PPP=0&reId=&boid={83989a39-81fd-43e3-8e97-a93a4e53a641}&date=8/3/2012",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
LAST);
		lr_end_sub_transaction("postClosePeriod_asp_5", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp_5", "OOTB_PostTimesheetHours");
			web_url("frmTimeSheet.asp_5",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20%20%20%20W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp_5", LR_AUTO);


	lr_end_transaction("OOTB_PostTimesheetHours",LR_AUTO);


	lr_think_time(15);


	lr_start_transaction("OOTB_SubmitHours");
		lr_start_sub_transaction("dsBookTime_asp_5", "OOTB_SubmitHours");
			web_submit_data("dsBookTime.asp_5",
				"Action=http://{server_server}/core/time_Sheet/dsBookTime.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20%20%20%20%20W",
				"Method=POST",
				"RecContentType=text/html",
				"Referer=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=        W",
				"Mode=HTTP",
				ITEMDATA,
				"Name=hidDescription", "Value=", ENDITEM,
				"Name=hidCurDate", "Value=8/3/2012", ENDITEM,
				"Name=hidRegularHours", "Value=8.00", ENDITEM,
				"Name=hidOverTimeHours", "Value=0.00", ENDITEM,
				"Name=hidCode1", "Value=", ENDITEM,
				"Name=hidCode2", "Value=", ENDITEM,
				"Name=hidCode3", "Value={67f64ba4-50b4-4d92-9e56-45a27f66fbca}", ENDITEM,
				"Name=hidGroupId", "Value={ec2353d1-3b9b-4075-bab5-fbb84529bacc}", ENDITEM,
				"Name=hidLocationId", "Value={949662a4-f4c3-403b-a293-8032e5e909ab}", ENDITEM,
				"Name=hidCategoryId", "Value={aa9c83c1-6e1e-4974-9dec-7c554cc429d2}", ENDITEM,
				"Name=hidCodeId", "Value={e716cde8-72a0-481d-9d29-99fbd74a9afa}", ENDITEM,
				"Name=hidTimeId", "Value=", ENDITEM,
				"Name=hidTaskId", "Value={{taskid_taskid}}", ENDITEM,
				"Name=hidTimeZone", "Value=", ENDITEM,
				"Name=hidTimeType", "Value=t", ENDITEM,
				"Name=hidAdjustedRegular", "Value=", ENDITEM,
				"Name=hidAdjustedOverTime", "Value=", ENDITEM,
				"Name=hidXCell", "Value=11", ENDITEM,
				"Name=hidYCell", "Value=2", ENDITEM,
				"Name=hidStartHour", "Value=", ENDITEM,
				"Name=hidStartMinute", "Value=", ENDITEM,
				"Name=hidPMAM", "Value=", ENDITEM,
				"Name=hidAction", "Value=add", ENDITEM,
				"Name=hidAdjustAction", "Value=", ENDITEM,
				"Name=hidAudit", "Value=0", ENDITEM,
				"Name=hidComment", "Value=", ENDITEM,
				"Name=hidbAdjustedTime", "Value=", ENDITEM,
				LAST);
		lr_end_sub_transaction("dsBookTime_asp_5", LR_AUTO);


		lr_start_sub_transaction("frmTimeSheet_asp_6", "OOTB_SubmitHours");
			web_url("frmTimeSheet.asp_6",
				"URL=http://{server_server}/core/time_Sheet/frmTimeSheet.asp?rid={{rid}}&reId={{rid}}&sno={{sno}}&ui=%20%20%20%20%20%20%20%20%20%20W",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("frmTimeSheet_asp_6", LR_AUTO);


		lr_start_sub_transaction("duTimesheet_asp", "OOTB_SubmitHours");
			web_url("duTimesheet.asp",
				"URL=http://{server_server}/Core/Time_Sheet/duTimesheet.asp?rid={{rid}}&reId=&sno={{sno}}&ui=W&showTask=&interval=&sdate=28&sdatem=7&sdatey=2012&lastpayday=6/30/2012",
				"Resource=0",
				"RecContentType=text/html",
				"Referer=http://{server_server}/Core/Time_Sheet/vwTimeSheet.asp?rid={{rid}}&sno={{sno}}&ui=W&reid=",
				"Mode=HTTP",
				LAST);
		lr_end_sub_transaction("duTimesheet_asp", LR_AUTO);


	lr_end_transaction("OOTB_SubmitHours",LR_AUTO);


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


	lr_end_transaction("uc_ootb_booksubmittime",LR_AUTO);

	return 0;
}
