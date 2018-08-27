# Welcome to PleTs repository!

PLeTs is an acronym for "Product Line of Testing Tools", which means that PLeTs is not a tool, but many! A very tiny overview on the PLeTs project is provided below.

**What is PLeTs?** PLeTs is a product line of testing tools. A software product line is a family of software products that have a common core. PLeTs provide a set of testing tools that aid in the automation of software testing.

**What kind of products does PLeTs provide?** For now, our project aims to provide tools for funcional, performance (web) and unitary testing. These tools do not execute test by themselves and, instead, they generate scripts for running is most of the renowed, on-the-shelf and FOSS tools.

**How does the products work?** All products are based on the MBT (model-based testing) approach. Usually, tools extract information from systems' models and generate testing scripts based on that information. 

**Is it possible to automate the generation of testing scripts from my legacy system models?** PLeTs products need an special notation on the models to properly treat system information. More information on supported models and syntax of PLeTs' notation can be found at [PLeTs Wiki](https://github.com/GiliSchmidt/PleTs-Testing/wiki) page.

**Which tools are currently supported?** PLeTs' products currently support the extraction of system information from UML models using XMI notation. We tested our parsers with ASTAH Professional and ArgoUML tools, but more tools may be supported. Regarding testing tools, testing scripts can be generated for Microsoft's VisualStudio 2010 Web Test, HP's LoadRunner, and JMeter tools. A more comprehensive overview on PLeTs features can be seen in the picture below.

![Image](https://raw.githubusercontent.com/GiliSchmidt/PleTs-Testing/master/resources/plets_img.png)
