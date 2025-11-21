using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Entities;
using Entities.DTO;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace UnitTests;

public class AvisoTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AvisoTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeveCriarEBuscarEDeletarAviso()
    {
        // ----------------------------------------
        // 1) Criar Aviso
        // ----------------------------------------
        var dto = new AvisoDto
        {
            Titulo = "Teste Automático",
            Mensagem = "Mensagem de Teste"
        };

        var createResponse = await _client.PostAsJsonAsync("/avisos", dto);
        createResponse.EnsureSuccessStatusCode();

        var criado = await createResponse.Content.ReadFromJsonAsync<Aviso>();
        Assert.NotNull(criado);
        Assert.True(criado.Id > 0);

        // ----------------------------------------
        // 2) Buscar pelo ID
        // ----------------------------------------
        var getResponse = await _client.GetAsync($"/avisos/{criado.Id}");
        getResponse.EnsureSuccessStatusCode();

        var encontrado = await getResponse.Content.ReadFromJsonAsync<Aviso>();
        Assert.NotNull(encontrado);
        Assert.Equal("Teste Automático", encontrado.Titulo);

        // ----------------------------------------
        // 3) Deletar
        // ----------------------------------------
        var deleteResponse = await _client.DeleteAsync($"/avisos/{criado.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        // ----------------------------------------
        // 4) Verificar que deve retornar 404 após deletar
        // ----------------------------------------
        var getDeleted = await _client.GetAsync($"/avisos/{criado.Id}");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, getDeleted.StatusCode);
    }

    [Fact]
    public async Task DeveListarAvisos()
    {
        // ----------------------------------------
        // Chama GET ALL
        // ----------------------------------------
        var response = await _client.GetAsync("/avisos");
        response.EnsureSuccessStatusCode();

        var lista = await response.Content.ReadFromJsonAsync<List<Aviso>>();
        Assert.NotNull(lista);

        // Se tiver 0 tá ok também
        Assert.True(lista.Count >= 0);
    }
}
