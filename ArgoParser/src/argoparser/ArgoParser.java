package argoparser;

import OgmaOATSParser.Parser;
import Servico.ArgoUMLtoAstahXML;
import java.awt.HeadlessException;
import java.io.IOException;
import javax.swing.JOptionPane;

public class ArgoParser {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            if (args.length != 1) {
                JOptionPane.showMessageDialog(null, "Parâmetro de entrada inválido.\n"
                        + "Especifique apenas o caminho para o arquivo", "ArgoParser", JOptionPane.ERROR_MESSAGE);
                return;
            }
            String path = args[0];
            Parser parser = new Parser();
            ArgoUMLtoAstahXML exporter = new ArgoUMLtoAstahXML();

            exporter.ToXmi(parser.ReadScript(path));
        } catch (HeadlessException | IOException | InterruptedException e) {
            JOptionPane.showMessageDialog(null, "Erro na execução do .JAR " + e, "ArgoParser", JOptionPane.ERROR_MESSAGE);
        }
    }
}
