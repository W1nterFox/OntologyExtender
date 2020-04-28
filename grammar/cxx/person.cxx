#encoding "utf8"
#GRAMMAR_ROOT S

S -> Person interp (PersonFact.FullName) ScienceDegree interp (PersonFact.ScienceDegree) AnyWord* Position interp (PersonFact.Position) EOSent;

Person -> Word<kwtype="фио">+;

ComplexAdj -> Adj<gnc-agr[1]> Adj<gnc-agr[1]>*;

ScienceDegree -> Noun<kwtype="тип_степени",gram="nom"> ComplexAdj<gnc-agr[1]> Noun<kwtype="наука",gnc-agr[1],gram="pl">;

Position -> Noun<kwtype="тип_должности">;