# Mappers

Mappers is lite way used to map/copy objects value from one source to another.

## Supports

1. Map object to object
2. Map DataTable to List of Object
3. Map distinct object to another by setting up the source

## Map

```csharp
Aluno alunoSource = new Aluno { Nome = "Math III" };
AlunoPessoa personTarget = new AlunoPessoa();
Mappers.Mapper.Map(alunoSource, personTarget); 
```

## Map distinct object to another by setting up the source

#### Setup mapping source

You can set up the source object to be mapped by string Name or using *intellisense*.

```c#
Mappers.SourceMappingKeys sourceSet = new Mappers.SourceMappingKeys();
 
sourceSet.Add("boarn", "dayBoarn");
 or
sourceSet.Add<StudantSource, StudantTarget>(x => x.name, y => y.StudantName);
```



#### Mapping

```csharp
StudantSource source = new StudantSource { name = "Fiado", gender = "4" };
StudantTarget target = new StudantTarget();

// inject mapping
Mappers.Mapper.Map(source, target, sourceSet);
```



## Exclude values

```csharp
Mappers.Mapper.Map(alunoSource, personTarget, propKeys => propKeys.alunoID);
```



#### Note:

The source file must have same structure or properties as the target is. 