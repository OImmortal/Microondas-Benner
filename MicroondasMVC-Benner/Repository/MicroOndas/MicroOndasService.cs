using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;
using System.Text.Json;

namespace MicroondasMVC_Benner.Repository.MicroOndas
{
    public class MicroOndasService : IMicroOndasInterface
    {

        public static MicroOndasModel microOndas = new MicroOndasModel();

        public PreAquecimentoModel? CadastrarPreAquecimento(PreAquecimentoModel model)
        {
            try
            {
                PreAquecimentoModel preAquecimento = new PreAquecimentoModel(model.Nome!, model.Alimento!, model.Tempo, model.Potencia, model.CaractereAquecimento!, model.InstrucoesComplementares);
                preAquecimento.id = microOndas.preAquecimentos.Count + 1;

                if (microOndas.ContainsCaracter(preAquecimento.CaractereAquecimento!)) throw new RegraMicroOndasException("Caracter de aquecimento já utilizado");

                if (preAquecimento.Tempo <= 0 || preAquecimento.Potencia <= 0) throw new RegraMicroOndasException("O tempo e a potencia devem ser maior que zero");

                if (preAquecimento.Potencia > 10) throw new RegraMicroOndasException("A potencia deve ser menor ou igual a 10"); ;

                if (string.IsNullOrWhiteSpace(preAquecimento.Nome) || string.IsNullOrWhiteSpace(preAquecimento.Alimento)) throw new RegraMicroOndasException("Nome e Alimento são obrigatórios"); ;

                preAquecimento.NovoItem = true;

                string caminhoJson = Path.Combine("Config", "PreAquecimentoItens.Json");

                string conteudo = System.IO.File.ReadAllText(caminhoJson);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Use as options no Deserialize
                List<PreAquecimentoModel> aquecimentos = [];

                if (conteudo != "")
                {
                    aquecimentos = JsonSerializer.Deserialize<List<PreAquecimentoModel>>(conteudo, options)!;
                }

                aquecimentos.Add(preAquecimento);

                string jsonString = JsonSerializer.Serialize(aquecimentos);

                System.IO.File.WriteAllText(caminhoJson, jsonString);

                microOndas.preAquecimentos.Add(preAquecimento);

                return preAquecimento;
            } catch (RegraMicroOndasException)
            {
                throw;
            }
        }

        public MicroOndasModel? IniciarOuAdicionar(int? Tempo, int? Potencia)
        {
            try
            {
                if (Tempo > 120 && Potencia > 10) throw new RegraMicroOndasException("tempo deve ser menor ou igual a 120 e a Potencia deve ser menor ou igual a 10");

                int tempoFinal = Tempo ?? 30;
                int potenciaFinal = Potencia ?? 10;

                microOndas.Iniciar(tempoFinal, potenciaFinal);

                return microOndas;
            }
            catch (RegraMicroOndasException) 
            {
                throw;
            }
            

        }

        public MicroOndasModel? IniciarPreAquecimento(int IdPreAquecimento)
        {
            try
            {
                PreAquecimentoModel preAquecimento = microOndas.preAquecimentos.Find(p => p.id == IdPreAquecimento);
                if (preAquecimento == null) throw new RegraMicroOndasException("PreAquecimento não encotrado");

                microOndas.IniciarPreAquecimento(IdPreAquecimento);
                return microOndas;
            } catch(RegraMicroOndasException) { throw; }
        }

        public MicroOndasModel? PausarOuCancelar(int tempo)
        {

            try
            {
                microOndas.PausarECancelar(tempo);

                return microOndas;
            } catch (RegraMicroOndasException) { throw; }
        }

    }
}
