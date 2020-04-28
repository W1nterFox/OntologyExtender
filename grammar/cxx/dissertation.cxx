#encoding "utf8"
#GRAMMAR_ROOT S

S -> DissertationDefenceYear AnyWord<~r-quoted,~l-quoted>* ScienceSpeciality;

S -> DissertationDefenceYear AnyWord* DissertationName AnyWord* ScienceSpeciality;

Title -> Word<h-reg1, l-quoted> Word<r-quoted>;

Title -> Word<h-reg1,l-quoted> AnyWord<~r-quoted>+ Word<r-quoted>;

Year -> AnyWord<wff=/^\d{4}$/> interp (DissertationFact.Year) Hyphen;

DissertationDefenceYear -> Year Verb<kwtype="защитил">;

DissertationName -> (Adj<kwtype="тип_диссертации",gnc-agr[1]>) Noun<kwtype="диссертация",gnc-agr[1]> Title<quoted> interp (DissertationFact.DissertationName::not_norm);

ComplexAdj -> Adj<gnc-agr[1]> Adj<gnc-agr[1]>*;

ScienceDegree -> Noun<kwtype="тип_степени",gram="gen"> ComplexAdj<gnc-agr[1]> Noun<kwtype="наука",gnc-agr[1],gram="pl">;

ScienceSpeciality -> ScienceDegree interp (DissertationFact.ScienceDegree) 'по' Noun<kwtype="направление",gram="dat"> AnyWord* Title<quoted> interp (DissertationFact.DissertationSpeciality::not_norm);