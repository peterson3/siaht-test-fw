//VERIFICADOR
try
{
Passou = true;
}
catch (Exception ex)
{
Passou = false;
}
//Se o resultado esperado é a FALHA, então inverta o sucesso.
if (Parametro.ToUpper() == "SIM")
{
Passou = Passou;
}
else //"NÃO"
{
Passou = !Passou;
}