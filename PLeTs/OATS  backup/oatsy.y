%{
#include <stdio.h>
#include <string.h>

extern yyparse();
extern FILE *yyin;

void yyerror(const char *str)
{
        fprintf(stderr,"error: %s\n",str);
}
 
int yywrap()
{
        return 1;
}

main(int argc, const char* argv[])
{
	printf("iniciando \n");

	FILE *myfile = fopen(argv[1], "r");
	yyin = myfile;

	yyparse();
}
%}

%token IMPORT PUBLIC CLASS EXTENDS SCRIPTSERVICE VOID THROWS IDENTIFIER UNKNOWN JAVADOCSTART JAVADOCEND ANY NUMBER THINK BEGINSTEP ENDSTEP WEB BROWSER WINDOW TEXTBOX BUTTON IMAGE ALERTDIALOG LINK ELEMENT LAUNCH

%%

oats:	 		list_import class
				| class
				;

epsilon:		;

list_import: 		import list_import
				| epsilon
				;

import:			IMPORT ANY ';'
				;

identifier:		IDENTIFIER identifier2
				;

identifier2:		'.' identifier
				| epsilon
				;

number:			NUMBER number2
				;

number2:		'.' NUMBER
				| epsilon
				;

class:			class_declaration '{' body '}'
				;

class_declaration:	PUBLIC CLASS IDENTIFIER EXTENDS IDENTIFIER
				;

body:			script_service_list methods
				| methods
				;

script_service_list:	script_service script_service_list
				| epsilon
				;

script_service:		SCRIPTSERVICE ANY ANY ';'
				;

methods:		javadoc methods2
				| methods2
				;

methods2:		method methods
				| epsilon
				;

any_javadoc:		ANY any_javadoc2
				;

any_javadoc2:		any_javadoc
				| epsilon
				;

javadoc:		JAVADOCSTART any_javadoc JAVADOCEND
				;

method:			method_declaration '{' method2
				;

method2:		block '}'
				| '}'
				;

method_declaration:	PUBLIC VOID IDENTIFIER '(' ')' method_declaration2
				;

method_declaration2:	THROWS IDENTIFIER
				| epsilon
				;

block:			script_element think block2
				| step block2
				;

block2:			block
				| epsilon
				;

think:			'{' THINK '(' number ')' ';' '}'
				| epsilon
				;

step:			begin_step step2
				;

step2:			'{' element_sequence '}' close_step
				| close_step
				;

begin_step:		BEGINSTEP '(' step_name ',' number ')' ';'
				;

step_name:		'"' step_name2
				;

step_name2:		ANY '"'
				| '"'
				;

close_step:		ENDSTEP '(' ')' ';'
				;

element_sequence:	script_element think element_sequence2
				;

element_sequence2:	element_sequence
				| epsilon
				;

element_name:		'"' ANY '"'
				;

// ACTION BLOCK
// DOCUMENTATION: TODO

action:			identifier '(' action2
				;

action2:		action3 ')'
				| ')'
				;

action3:		identifier action4
				| '"' ANY '"'
				;

action4:		'(' action2
				| epsilon
				;

// ACTION BLOCK

script_element:		WEB '.' web
				| BROWSER '.' browser
				;

element_details:	'(' number ',' element_name ')' '.' action
				;

web:			wwindow
				| wtextbox
				| wbutton
				| wimage
				| walertdialog
				| wlink
				| welement
				;

wwindow:		WINDOW element_details ';'
				;

wtextbox:		TEXTBOX element_details ';'
				;

wbutton:		BUTTON element_details ';'
				;

wimage:			IMAGE element_details ';'
				;

walertdialog:		ALERTDIALOG element_details ';'
				;

wlink:			LINK element_details ';'
				;

welement:		ELEMENT element_details ';'
				;

browser:		blaunch
				;

blaunch:		LAUNCH '(' ')' ';'
				;

%%
