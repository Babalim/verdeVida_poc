verdeVida_poc
este repositório contém a PoC do projeto integrado multidiciplinar (PIM), da nossa fazenda urbana Verde Vida, a Poc verdeViva inclui a estrutura do banco de dados desenvolvido no SQL Server e o código fonte desenvolvido em C# utilizando o .NET framework.

estrutura do repositório
o repositório está organizado da seguinte forma:

/verdeVida
/bin/Debug: Contém os binários compilados do projeto.
verdeVida.exe
verdeVida.exe.config
verdeVida.pdb

/obj/Debug: Contém arquivos de objeto e cache gerados durante a compilação.
DesignTimeResolveAssemblyReferencesInput.cache
verdeVida.csproj.AssemblyReference.cache
verdeVida.csproj.CoreCompileInputs.cache
verdeVida.csproj.FileListAbsolute.txt
verdeVida.exe
verdeVida.pdb

/Properties: Contém informações de configuração do assembly.
AssemblyInfo.cs
App.config: Arquivo de configuração da aplicação.
Program.cs: Arquivo principal do programa em C#.
verdeVida.csproj: Arquivo de projeto do C#.
verdeVida.sln: Solução do Visual Studio para o projeto verdeVida.
verdeVida.sql: Script SQL para criação e estruturação do banco de dados verdeVida no SQL Server.
verdeVida.ssmssqlproj: Arquivo de projeto do SQL Server Management Studio.

Descrição dos Componentes
Banco de Dados
O arquivo verdeVida.sql na raiz do diretório principal é o script SQL necessário para a criação e estruturação do banco de dados verdeVida no SQL Server. Certifique-se de executar este script em um ambiente de banco de dados SQL Server compatível antes de iniciar o projeto.

Código Fonte
O código fonte do projeto verdeVida está localizado no diretório principal e é desenvolvido em C# com .NET. Este código é responsável pela lógica de aplicação e deve ser aberto e executado no Visual Studio.

App.config: Arquivo de configuração da aplicação.
Program.cs: Arquivo principal que contém o ponto de entrada do programa.
verdeVida.csproj: Arquivo de projeto do C# que define como o projeto deve ser compilado.
verdeVida.sln: Arquivo de solução do Visual Studio que agrupa os projetos relacionados.
