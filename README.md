**MUTANT**

API que permite detectar si un humano es mutante, guardar la información correspondiente y permitir consultar las estadísticas de las verificaciones de ADN

#### *Criterios de aceptación*
1. El ADN no puede contener letras diferentes a A, C, G, T
2. La cantidad de caracteres que represente una secuencia debe de ser equivalente a la cantidad de sequencias a evaluar por humano

#### **Ejemplos**

#### *- Url para evaluar un mutante*
https://mutanttest.azurewebsites.net/Mutant

#### - Tipo
POST

#### - Requet
``` json
{
  "dna": [
    "ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"
  ]
}
```

#### - Response
- **200** Cuando el ADN evaluado corresponda a un mutante
- **400** En caso de que el ADN no cumpla con los criterios para poder evaluarlo
- **403** Cuando el ADN evaluado no corresponda a un mutante

#### *- Url para estadísticas*
https://mutanttest.azurewebsites.net/Stats

#### - Tipo
GET

#### - Response
``` json
{
    "count_mutant_dna": 1,
    "count_human_dna": 1,
    "ratio": 1
}
```