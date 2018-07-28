package argoparser;

import OgmaOATSParser.Parser;
import Servico.ArgoUMLtoAstahXML;
import java.awt.HeadlessException;
import java.io.IOException;
import javax.swing.JOptionPane;

/**
 *
 * @author Giliardi Schmidt
 */
public class COMMANDLINE_ArgoParser {

    public static void main(String[] args) {
        String path = "C:\\Users\\gilis\\Desktop\\Pesquisa\\PleTs\\PLeTs\\__utests\\TestFiles\\java oats script.java";
        try {

            Parser parser = new Parser();
            ArgoUMLtoAstahXML exporter = new ArgoUMLtoAstahXML();

            exporter.ToXmi(parser.ReadScript(path));
        } catch (HeadlessException | IOException | InterruptedException e) {
            JOptionPane.showMessageDialog(null, "Erro na execução do .JAR " + e, "ArgoParser", JOptionPane.ERROR_MESSAGE);
        }
    }
}
