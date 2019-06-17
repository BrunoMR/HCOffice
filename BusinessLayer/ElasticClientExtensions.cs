namespace BusinessLayer
{
    using Nest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utils;
    using System.Configuration;
    using DTOLayer.Indexes;

    public static class ElasticClientExtensions
    {
        public static void CreateIndexByName(this IElasticClient elasticClient, string index)
        {
            if (elasticClient.IndexExists(index.ToLower()).Exists == false)
                VerifyMainIndex(elasticClient, index);
            //elasticClient.VerifyMainIndex(index);
        }

        private static void VerifyMainIndex(IElasticClient elasticClient, string index)
        {
            if (index.Equals(ConfigurationManager.AppSettings["ElasticMainIndex"]))
                elasticClient.CreateIndex(index, c => c
                    .Settings(st => st
                        .Analysis(an => an
                            .Analyzers(anz => anz
                                .Custom("case_insensitive", ci => ci
                                    .Tokenizer("standard")
                                    .Filters("uppercase", "asciifolding"))
                                //.Custom("not_analyzed", ci => ci
                                //    .Tokenizer("not_analyzed")
                                //    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_1", cn1 => cn1
                                    .Tokenizer("ngram_with_1")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_2", cn2 => cn2
                                    .Tokenizer("ngram_with_2")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_3", cn3 => cn3
                                    .Tokenizer("ngram_with_3")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_4", cn4 => cn4
                                    .Tokenizer("ngram_with_4")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_6", cn6 => cn6
                                    .Tokenizer("ngram_with_6")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_7", cn7 => cn7
                                    .Tokenizer("ngram_with_7")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_8", cn8 => cn8
                                    .Tokenizer("ngram_with_8")
                                    .Filters("uppercase", "asciifolding"))
                                .Custom("ngram_with_9", cn9 => cn9
                                    .Tokenizer("ngram_with_9")
                                    .Filters("uppercase", "asciifolding"))
                                )
                           .Tokenizers(tz => tz
                                .NGram("ngram_with_1", tk => tk
                                    .MinGram(1)
                                    .MaxGram(1)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_2", tk => tk
                                    .MinGram(2)
                                    .MaxGram(2)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_3", tk => tk
                                    .MinGram(3)
                                    .MaxGram(3)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_4", tk => tk
                                    .MinGram(4)
                                    .MaxGram(4)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_6", tk => tk
                                    .MinGram(6)
                                    .MaxGram(6)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_7", tk => tk
                                    .MinGram(7)
                                    .MaxGram(7)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_8", tk => tk
                                    .MinGram(8)
                                    .MaxGram(8)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                .NGram("ngram_with_9", tk => tk
                                    .MinGram(9)
                                    .MaxGram(9)
                                    .TokenChars(
                                        TokenChar.Digit,
                                        TokenChar.Letter))
                                      )))
                  .Mappings(mp => mp
                    .Map<ProcessoIndex>("processo", ps => ps
                        .AutoMap()
                        .Properties(pt => pt
                            .String(sg => sg
                                .Name(n => n.Marca)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.Marca)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("not_analyzed")
                                        .NotAnalyzed())
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))))
                            .String(sg => sg
                                .Name(n => n.Titular)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.Titular)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("not_analyzed")
                                        .NotAnalyzed())
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))))
                            .String(sg => sg
                                .Name(n => n.Procurador)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.Procurador)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("not_analyzed")
                                        .NotAnalyzed())
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))))
                            .String(sg => sg
                                .Name(n => n.Apostila)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.Apostila)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))))
                            .String(sg => sg
                                .Name(n => n.Especificacao)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.Especificacao)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))))
                            .String(sg => sg
                                .Name(n => n.MarcaOrtografada)
                                .Fields(fi => fi
                                    .String(sf => sf
                                        .Name(n => n.MarcaOrtografada)
                                        .Analyzer("standard"))
                                    .String(sf => sf
                                        .Name("case_insensitive")
                                        .Analyzer("case_insensitive"))
                                    .String(sf => sf
                                        .Name("ngram_with_1")
                                        .Analyzer("ngram_with_1"))
                                    .String(sf => sf
                                        .Name("ngram_with_2")
                                        .Analyzer("ngram_with_2"))
                                    .String(sf => sf
                                        .Name("ngram_with_3")
                                        .Analyzer("ngram_with_3"))
                                    .String(sf => sf
                                        .Name("ngram_with_4")
                                        .Analyzer("ngram_with_4"))
                                    .String(sf => sf
                                        .Name("ngram_with_6")
                                        .Analyzer("ngram_with_6"))
                                    .String(sf => sf
                                        .Name("ngram_with_7")
                                        .Analyzer("ngram_with_7"))
                                    .String(sf => sf
                                        .Name("ngram_with_8")
                                        .Analyzer("ngram_with_8"))
                                    .String(sf => sf
                                        .Name("ngram_with_9")
                                        .Analyzer("ngram_with_9"))))
                                .String(sg => sg
                                    .Name(n => n.MarcaNaoOrtografada)
                                    .Fields(fi => fi
                                        .String(sf => sf
                                            .Name(n => n.MarcaNaoOrtografada)
                                            .Analyzer("standard"))
                                        .String(sf => sf
                                            .Name("case_insensitive")
                                            .Analyzer("case_insensitive"))
                                        .String(sf => sf
                                            .Name("ngram_with_1")
                                            .Analyzer("ngram_with_1"))
                                        .String(sf => sf
                                            .Name("ngram_with_2")
                                            .Analyzer("ngram_with_2"))
                                        .String(sf => sf
                                            .Name("ngram_with_3")
                                            .Analyzer("ngram_with_3"))
                                        .String(sf => sf
                                            .Name("ngram_with_4")
                                            .Analyzer("ngram_with_4"))
                                        .String(sf => sf
                                            .Name("ngram_with_6")
                                            .Analyzer("ngram_with_6"))
                                        .String(sf => sf
                                            .Name("ngram_with_7")
                                            .Analyzer("ngram_with_7"))
                                        .String(sf => sf
                                            .Name("ngram_with_8")
                                            .Analyzer("ngram_with_8"))
                                        .String(sf => sf
                                            .Name("ngram_with_9")
                                            .Analyzer("ngram_with_9"))))
                                .String(sg => sg
                                    .Name(n => n.MarcaNaoOrtografadaNoSpace)
                                    .Fields(fi => fi
                                        .String(sf => sf
                                            .Name(n => n.MarcaNaoOrtografadaNoSpace)
                                            .Analyzer("standard"))
                                        .String(sf => sf
                                            .Name("case_insensitive")
                                            .Analyzer("case_insensitive"))
                                        .String(sf => sf
                                            .Name("ngram_with_1")
                                            .Analyzer("ngram_with_1"))
                                        .String(sf => sf
                                            .Name("ngram_with_2")
                                            .Analyzer("ngram_with_2"))
                                        .String(sf => sf
                                            .Name("ngram_with_3")
                                            .Analyzer("ngram_with_3"))
                                        .String(sf => sf
                                            .Name("ngram_with_4")
                                            .Analyzer("ngram_with_4"))
                                        .String(sf => sf
                                            .Name("ngram_with_6")
                                            .Analyzer("ngram_with_6"))
                                        .String(sf => sf
                                            .Name("ngram_with_7")
                                            .Analyzer("ngram_with_7"))
                                        .String(sf => sf
                                            .Name("ngram_with_8")
                                            .Analyzer("ngram_with_8"))
                                        .String(sf => sf
                                            .Name("ngram_with_9")
                                            .Analyzer("ngram_with_9"))))
                                    .Object<ClasseSync>(cl => cl
                                        .AutoMap()
                                        .Name(pi => pi.Classe)
                                        .Properties(pc => pc
                                            .String(sc => sc
                                                .Name(nc => nc.Descricao)
                                                .Fields(fc => fc
                                                    .String(scp => scp
                                                        .Name(nc => nc.Descricao)
                                                        .Analyzer("standard"))
                                                    .String(scp => scp
                                                        .Name("case_insensitive")
                                                        .Analyzer("case_insensitive"))
                                                        ))))
                                    .Object<DespachoSync>(di => di
                                        .AutoMap()
                                        .Name(pi => pi.Despacho)
                                        .Properties(pd => pd
                                            .String(sd => sd
                                                .Name(nd => nd.Complemento)
                                                .Fields(fd => fd
                                                    .String(sdp => sdp
                                                        .Name(nd => nd.Complemento)
                                                        .Analyzer("standard"))
                                                    .String(sdp => sdp
                                                        .Name("case_insensitive")
                                                        .Analyzer("case_insensitive"))
                                                        ))
                                            .String(sd => sd
                                                .Name(nd => nd.DescricaoCompleta)
                                                .Fields(fd => fd
                                                    .String(sdp => sdp
                                                        .Name(nd => nd.DescricaoCompleta)
                                                        .Analyzer("standard"))
                                                    .String(sdp => sdp
                                                        .Name("case_insensitive")
                                                        .Analyzer("case_insensitive"))
                                                        ))))
                     ))));
            else
                elasticClient.CreateIndex(index);

        }

        public static long GetCountAllProcesses<T>(this IElasticClient elasticClient) where T : class
        {
            var result = elasticClient.Search<T>(s =>
                    s.MatchAll()
                    .Take(1)).Total.ToString();

            return long.Parse(result);
        }

        public static ISearchResponse<T> FilterBy<T>(this IElasticClient elasticClient, Expression<Func<T, object>> expression, object value) where T : class
        {
            var field = expression.GetPropertyName();

            return elasticClient.Search<T>(s =>
                    s.Query(q => MatchField(field, value))
                    .Skip(0)
                    .Take(100));

        }

        public static IEnumerable<T> FilterBy<T>(this IElasticClient elasticClient, string field, object value) where T : class
        {
            var result = elasticClient.Search<T>(s =>
                    s.Query(q => MatchField(field, value))
                    .Skip(0)
                    .Take(100));

            var list = result.Hits.Select(h => h.Source);

            return list;
        }

        public static ISearchResponse<T> FilterBy<T>(this IElasticClient elasticClient, SearchRequest searchRequest) where T : class
        {
            return elasticClient.Search<T>(searchRequest);
        }

        private static MatchQuery MatchField(string field, object value)
        {
            return new MatchQuery { Field = field.ToLowerFirstChar(), Query = value.ToString() };
        }
    }
}
