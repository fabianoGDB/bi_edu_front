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

    // opcionais no card
    public string? FotoUrl { get; set; }
    public double? Frequencia { get; set; }
    public string? Situacao { get; set; }

    public List<AlunoFato> Fatos { get; set; } = new();
}

public class AlunoFato
{
    public string Disciplina { get; set; } = "";
    public string Area { get; set; } = "";
    public int PeriodoAvaliativoId { get; set; } // 1..4 (Rec=5 opcional)
    public decimal? Nota { get; set; }
    public int? Faltas { get; set; }
    public string? Situacao { get; set; }
}

public class BimestreResumoDto
{
    public int Bimestre { get; set; }            // 1..4, 5=Rec, 6=Ano (se vier)
    public string Area { get; set; } = "";
    public decimal? Media { get; set; }
}
public class ObservacaoItem
{
    public string Codigo { get; set; } = "";  // ex.: MAT1234
    public string Texto { get; set; } = "";
    public string Autor { get; set; } = "";  // ex.: Prof. Silva
    public DateTime? Data { get; set; }       // UTC

    // Compat (alguns componentes legados usam "Professor")
    public string Professor
    {
        get => Autor;
        set => Autor = value ?? "";
    }
}

// Página de observações (se houver paginação)
public class ObservacaoPage
{
    public List<ObservacaoItem> Items { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

// DTO para criação (caso venha a usar POST JSON)
public class ObservacaoCreate
{
    public string Codigo { get; set; } = "";
    public string Texto { get; set; } = "";
    public string Autor { get; set; } = "";
    public DateTime? Data { get; set; }
}


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


public class NotasGrid
{
    public List<string> Disciplinas { get; set; } = new();
    public List<NotasLinha> Linhas { get; set; } = new();
}

public class NotasLinha
{
    public string Bim { get; set; } = ""; // "1º", "2º", "3º", "4º", "Ano"
    public Dictionary<string, decimal?> Notas { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}