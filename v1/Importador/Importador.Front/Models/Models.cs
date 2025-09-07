namespace Importador.Front.Models
{
    public class ImportListItem
    {
        public Guid Id { get; set; }
        public string? OriginalFileName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int Alunos { get; set; }
        public int Status { get; set; } // 0 pendente, 1 processando, 2 finalizado, 3 erro
        public string? Error { get; set; }
    }

    public class ImportStatus
    {
        public Guid Id { get; set; }
        public List<ImportStage> Stages { get; set; } = new();
    }

    public class ImportStage
    {
        public string Name { get; set; } = "";
        public int Status { get; set; }        // 0 aguardando, 1 em progresso, 2 concluída, 3 erro
        public int ProcessedRows { get; set; }
    }

    public class AlunoListItem
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Matricula { get; set; }
    }

    public sealed class AlunoDetalhe
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Matricula { get; set; }

        // >>> ADIÇÕES que sua página usa <<<
        public string? FotoUrl { get; set; }         // URL da foto do aluno
        public double? Frequencia { get; set; }      // 0..100 (%)
        public string? Situacao { get; set; }        // "APR", "REP", etc.

        public List<AlunoFato> Fatos { get; set; } = new();

        // Opcional para o bloco de observações
        public List<ObservacaoPedagogica> Observacoes { get; set; } = new();
    }

    public sealed class AlunoFato
    {
        public string Disciplina { get; set; } = "";
        public int PeriodoAvaliativoId { get; set; } // 1..4
        public decimal? Nota { get; set; }
        public string? Situacao { get; set; }        // APR/REP/…
    }


    public class Observacao
    {
        public string Disciplina { get; set; } = "";
        public string Texto { get; set; } = "";
        public string Professor { get; set; } = "";
        public DateTime Data { get; set; }
    }

    public sealed class ObservacaoPedagogica
    {
        public string? Disciplina { get; set; }  // ex.: "MAT12345"
        public string? Texto { get; set; }       // o conteúdo da observação
        public string? Autor { get; set; }       // ex.: "Prof. Silva"
        public DateTime? Data { get; set; }      // quando foi registrada
    }
}