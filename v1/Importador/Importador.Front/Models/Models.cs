// Importador.Front/Models/Models.cs
using System;
using System.Collections.Generic;

namespace Importador.Front.Models
{
    // ============= ALUNOS / CONSELHO =============
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

        // Card (opcionais)
        public string? FotoUrl { get; set; }
        public double? Frequencia { get; set; }
        public string? Situacao { get; set; }

        // Fatos (linhas da fato_nota projetadas)
        public List<AlunoFato> Fatos { get; set; } = new();
    }

    public class AlunoFato
    {
        public string Disciplina { get; set; } = "";
        public string Area { get; set; } = "";
        public int PeriodoAvaliativoId { get; set; } // 1..4 (5=Rec opcional)
        public decimal? Nota { get; set; }
        public int? Faltas { get; set; }
        public string? Situacao { get; set; } // APR/REP/…
    }

    public class BimestreResumoDto
    {
        public int Bimestre { get; set; } // 1..4, 5=Rec, 6=Ano
        public string Area { get; set; } = "";
        public decimal? Media { get; set; }
    }

    public class NotasGrid
    {
        public List<string> Disciplinas { get; set; } = new();
        public List<NotasLinha> Linhas { get; set; } = new();
    }

    public class NotasLinha
    {
        // Ex.: "1º", "2º", "3º", "4º", "Ano"
        public string Bim { get; set; } = "";
        public Dictionary<string, decimal?> Notas { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);
    }

    // ============= OBSERVAÇÕES (CONSELHO) =============
    public class ObservacaoItem
    {
        public string Codigo { get; set; } = "";   // ex.: MAT1234
        public string Texto { get; set; } = "";
        public string Autor { get; set; } = "";    // ex.: Prof. Silva
        public DateTime? Data { get; set; }        // UTC

        // Compat (alguns componentes legados usam "Professor")
        public string Professor
        {
            get => Autor;
            set => Autor = value ?? "";
        }
    }

    public class ObservacaoPage
    {
        public List<ObservacaoItem> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class ObservacaoCreate
    {
        public string Codigo { get; set; } = "";
        public string Texto { get; set; } = "";
        public string Autor { get; set; } = "";
        public DateTime? Data { get; set; }
    }

    // ============= IMPORTAÇÕES (UPLOAD/PROCESS) =============
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

    // ============= ÁREAS =============
    public sealed class AreaListItem
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string CorHex { get; set; } = "#3B82F6";
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public int DisciplinasCount { get; set; }
    }

    public sealed class AreaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string CorHex { get; set; } = "#3B82F6";
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
    }
   

    // Mantido por compatibilidade com versões citadas (mesmo shape de AreaUpsertDto)
    public sealed class AreaUpsert
    {
        public string Nome { get; set; } = "";
        public string CorHex { get; set; } = "#3B82F6";
        public int Ordem { get; set; } = 1;
        public bool Ativo { get; set; } = true;
    }

    // ============= DISCIPLINAS =============
    // Página de listagem com paginação (usada em Disciplinas.razor)
    public sealed class DisciplinaPage
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<DisciplinaListItem> Items { get; set; } = new();
    }

    // Mantido caso alguma parte do código antigo espere esse nome
    public sealed class DisciplinaListPage
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<DisciplinaListItem> Items { get; set; } = new();
    }

    public sealed class DisciplinaListItem
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Sigla { get; set; } = "";
        public int AreaId { get; set; }
        public string AreaNome { get; set; } = "";
        public string AreaCorHex { get; set; } = "#3B82F6";
        public string? CargaHorariaRotulo { get; set; }
    }

    public sealed class DisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Sigla { get; set; } = "";
        public int AreaId { get; set; }
        public string AreaNome { get; set; } = "";
        public string AreaCorHex { get; set; } = "#3B82F6";
        public string? CargaHorariaRotulo { get; set; }
    }


    // Mantido por compatibilidade com versões citadas
    public sealed class DisciplinaUpsert
    {
        public string Nome { get; set; } = "";
        public string Sigla { get; set; } = "";
        public int AreaId { get; set; }
        public string? CargaHorariaRotulo { get; set; }
    }
}
