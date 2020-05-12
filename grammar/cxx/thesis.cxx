#encoding "utf8"
#GRAMMAR_ROOT S

S -> ThesisDefence | Scientist | Department;

Title -> Word<h-reg1, l-quoted> Word<r-quoted>;

Title -> Word<h-reg1,l-quoted> AnyWord<~r-quoted>+ Word<r-quoted>;

Year -> AnyWord<wff=/^\d{4}$/>;

DissertationDefenceYear -> Year Hyphen "защитить";

DissertationName -> (Adj<kwtype="тип_диссертации",gnc-agr[1]>) "диссертация"<gnc-agr[1]> Title<quoted> interp (ThesisFact.Thesis::not_norm);

ComplexAdj -> Adj<gnc-agr[1]> Adj<gnc-agr[1]>*;

AcademicDegree -> Noun<kwtype="тип_степени",gram="gen"> ComplexAdj<gnc-agr[1]> interp (ThesisFact.BranchOfScience::norm="nom,pl") "наука"<gnc-agr[1],gram="pl">;

ScienceSpeciality -> AcademicDegree interp (ThesisFact.AcademicDegree) "по" Noun<kwtype="направление",gram="dat"> AnyWord* Title<quoted> interp (ThesisFact.Speciality::not_norm);

Cathedra -> "кафедра" Title;

Department -> Cathedra interp (ThesisFact.Department::not_norm) Hyphen Position;

Person -> Word<kwtype="фио">+;

Position -> Noun<kwtype="тип_должности">;

ThesisDefence -> DissertationDefenceYear AnyWord* DissertationName AnyWord* ScienceSpeciality;

ThesisDefence -> DissertationDefenceYear AnyWord<~r-quoted,~l-quoted>* ScienceSpeciality;

Scientist -> Person interp (ThesisFact.Scientist) AnyWord* Position EOSent;