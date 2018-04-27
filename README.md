# Mappers

Mappers is lite way used to map/copy objects value from one source to another.



## Object Initialization

```csharp
Mappers.Mapper mapper = new Mappers.Mapper();
```



## Map

```csharp
Aluno alunoSource = new Aluno { Nome = "Math III" };
AlunoPessoa personTarget = new AlunoPessoa();
mapper.Map(alunoSource, personTarget); 
```



## Exclude values

```csharp
mapper.Map(alunoSource, personTarget, propKeys => propKeys.alunoID);
```



#### Note:

The source file must have same structure or properties as the target is. 