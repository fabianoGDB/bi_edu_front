namespace Importador.Front.Models;

public class AlunoListItem
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Matricula { get; set; }
}

public class AlunoDetalhe
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Matricula { get; set; }

    // >>> Necessário pelos componentes de Conselho
    public List<AlunoFato> Fatos { get; set; } = new();
}

public class AlunoFato
{
    public string Disciplina { get; set; } = "";
    public int PeriodoAvaliativoId { get; set; } // 1..4 (Rec se aplicável)
    public decimal? Nota { get; set; }
    public string? Situacao { get; set; } // APR/REP/...
}

// Imports (mantidos se já existiam)
public class ImportListItem
{
    public Guid Id { get; set; }
    public string? OriginalFileName { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public int Alunos { get; set; }
    public int Status { get; set; }
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

public class ObservacaoItem
{
    public string Codigo { get; set; } = "";  // ex.: MAT1234
    public string Texto { get; set; } = "";
    public string Autor { get; set; } = "";  // ex.: Prof. Silva
    public DateTime? Data { get; set; }       // UTC

    // >>> Compat: alguns componentes antigos referem-se a "Professor"
    public string Professor
    {
        get => Autor;
        set => Autor = value ?? "";
    }
}