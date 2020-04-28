#encoding "utf8"
#GRAMMAR_ROOT S

S -> GraduationYear FacultyNoun interp (GraduationFact.GraduateFaculty) UniversityNoun interp (GraduationFact.GraduateUniversity);

Year -> AnyWord<wff=/^\d{4}$/> interp (GraduationFact.Year) Hyphen;

GraduationYear -> Year Verb<kwtype="окончил">;

ComplexAdj -> Adj<gnc-agr[1]> Adj<gnc-agr[1]>*;

FacultyNoun -> ComplexAdj<gnc-agr[1]> Noun<kwtype="факультет",gnc-agr[1]>;

UniversityNoun -> ComplexAdj<gnc-agr[1]> Noun<kwtype="университет",gnc-agr[1]>;