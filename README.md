# Mappers

Mappers is lite way used to map/copy objects value from one source to another.



## Map

```csharp
Aluno alunoSource = new Aluno { Nome = "Math III" };
AlunoPessoa personTarget = new AlunoPessoa();
Mappers.Mapper.Map(alunoSource, personTarget); 
```



## Exclude values

```csharp
Mappers.Mapper.Map(alunoSource, personTarget, propKeys => propKeys.alunoID);
```



#### Note:

The source file must have same structure or properties as the target is. 