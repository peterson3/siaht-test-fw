//VERIFICADOR
try
{
Passou = true;
}
catch (Exception ex)
{
Passou = false;
}
//Se o resultado esperado � a FALHA, ent�o inverta o sucesso.
if (Parametro.ToUpper() == "SIM")
{
Passou = Passou;
}
else //"N�O"
{
Passou = !Passou;
}