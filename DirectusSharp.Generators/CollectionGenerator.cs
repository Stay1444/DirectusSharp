using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DirectusSharp.Generators;

[Generator]
public class CollectionGenerator : ISourceGenerator
{
    private const string AttributeName = "DirectusSharp.Generators.Attributes.DirectusCollectionAttribute";
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            return;

        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            var model = context.Compilation.GetSemanticModel(classDeclaration.SyntaxTree);
            var classSymbol = model.GetDeclaredSymbol(classDeclaration);

            if (classSymbol == null || !classSymbol.GetAttributes()
                    .Any(attr => attr.AttributeClass?.ToDisplayString() == AttributeName))
            {
                continue;
            }
                
            var className = classSymbol.Name;
                
            var idProperty = classDeclaration.Members
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(p => p.Identifier.Text == "Id" || p.Identifier.Text == $"{className}Id");

            if (idProperty is null) continue;
            if (model.GetDeclaredSymbol(idProperty) is not IPropertySymbol idPropertySymbol) continue;
            
            var attributeData = classSymbol.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == AttributeName);
            
            if (attributeData is null) continue;
            
            var firstAttributeArg = attributeData.ConstructorArguments.FirstOrDefault();
            if (firstAttributeArg.Value is not string collectionName) continue;
            
            var source = $@"
using DirectusSharp.Requests.Collections;

namespace {classSymbol.ContainingNamespace};

public static class {className}Collection
{{
    public class Get{className}Request : GetItemRequest<{classSymbol.ToDisplayString()}> {{
        public required {idPropertySymbol.Type.ToDisplayString()} {idProperty.Identifier.Text} {{ get; init; }}

        protected override string GetCollection() => ""{collectionName}"";
        protected override string GetItemId() => {idProperty.Identifier.Text}.ToString();
    }}

    public class GetMultiple{className}Request : GetItemsRequest<{classSymbol.ToDisplayString()}> {{
        protected override string GetCollection() => ""{collectionName}"";
    }}

    public class Create{className}Request : CreateItemRequest<{classSymbol.ToDisplayString()}> {{
        public required {classSymbol.ToDisplayString()} {className} {{ get; init; }}

        protected override string GetCollection() => ""{collectionName}"";
        protected override {classSymbol.ToDisplayString()} GetItem() => {className};
    }}

    public class Update{className}Request : UpdateItemRequest<{classSymbol.ToDisplayString()}> {{
        public required {classSymbol.ToDisplayString()} {className} {{ get; init; }}

        protected override string GetCollection() => ""{collectionName}"";
        protected override {classSymbol.ToDisplayString()} GetItem() => {className};
        protected override string GetItemId() => {className}.{idProperty.Identifier.Text}.ToString();
    }}

    public class Delete{className}Request : DeleteItemRequest {{
        public required {idPropertySymbol.Type.ToDisplayString()} {idProperty.Identifier.Text} {{ get; init; }}

        protected override string GetCollection() => ""{collectionName}"";
        protected override string GetItemId() => {idProperty.Identifier.Text}.ToString();
    }}

    public class DeleteMultiple{className}Request : DeleteItemsRequest {{
        public required {idPropertySymbol.Type.ToDisplayString()}[] {idProperty.Identifier.Text} {{ get; init; }}

        protected override string GetCollection() => ""{collectionName}"";
        protected override string[] GetItemIds() => {idProperty.Identifier.Text}.Select(x => x.ToString()).ToArray();
    }}

    public static async Task<{classSymbol.ToDisplayString()}?> Get{className}Async(this DirectusSharp.IDirectus client, {idPropertySymbol.Type.ToDisplayString()} {idProperty.Identifier.Text.ToCamelCase()}) {{
        var response = await client.ExecuteAsync(new Get{className}Request() {{
            {idProperty.Identifier.Text} = {idProperty.Identifier.Text.ToCamelCase()}
        }});

        if (response.IsSuccess) return response.Data;
        return null;
    }}

    public static async Task<{classSymbol.ToDisplayString()}[]> GetMultiple{className}Async(this DirectusSharp.IDirectus client) {{
        var response = await client.ExecuteAsync(new GetMultiple{className}Request() {{}});

        if (response.IsSuccess) return response.Data;
        return null;
    }}

    public static async Task<{classSymbol.ToDisplayString()}?> Create{className}Async(this DirectusSharp.IDirectus client, {classSymbol.ToDisplayString()} {className.ToCamelCase()}) {{
        var response = await client.ExecuteAsync(new Create{className}Request() {{
            {className} = {className.ToCamelCase()}
        }});

        if (response.IsSuccess) return response.Data;
        return null;
    }}

    public static async Task<{classSymbol.ToDisplayString()}?> Update{className}Async(this DirectusSharp.IDirectus client, {classSymbol.ToDisplayString()} {className.ToCamelCase()}) {{
        var response = await client.ExecuteAsync(new Update{className}Request() {{
            {className} = {className.ToCamelCase()}
        }});

        if (response.IsSuccess) return response.Data;
        return null;
    }}

    public static async Task Delete{className}Async(this DirectusSharp.IDirectus client, {idPropertySymbol.Type.ToDisplayString()} {idProperty.Identifier.Text.ToCamelCase()}) {{
        var response = await client.ExecuteAsync(new Delete{className}Request() {{
            {idProperty.Identifier.Text} = {idProperty.Identifier.Text.ToCamelCase()}
        }});
    }}

    public static async Task Delete{className}Async(this DirectusSharp.IDirectus client, params System.Collections.Generic.IEnumerable<{idPropertySymbol.Type.ToDisplayString()}> {idProperty.Identifier.Text.ToCamelCase()}) {{
        var response = await client.ExecuteAsync(new DeleteMultiple{className}Request() {{
            {idProperty.Identifier.Text} = {idProperty.Identifier.Text.ToCamelCase()}.ToArray()
        }});
    }}
}}
";
            context.AddSource($"{className}_Generated.cs", source);
        }
    }

    private class SyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClasses { get; } = [];

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax { AttributeLists.Count: > 0 } classDeclaration)
                CandidateClasses.Add(classDeclaration);
        }
    }
}