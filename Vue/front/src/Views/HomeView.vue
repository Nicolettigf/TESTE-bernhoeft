  <template>
    <div class="container">
      <h1>Dashboard de Avisos</h1>
  
      <button @click="$router.push('/avisos')">
        ➕ Criar Novo Aviso
      </button>
  
      <div v-if="loading">Carregando...</div>
  
      <table v-else>
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Mensagem</th>
            <th>Criado em</th>
            <th>Ações</th>
          </tr>
        </thead>
  
        <tbody>
          <tr v-for="a in avisos" :key="a.id">
            <td>{{ a.id }}</td>
            <td>{{ a.titulo }}</td>
            <td>{{ a.mensagem }}</td>
            <td>{{ new Date(a.criadoEm).toLocaleString() }}</td>
            <td>
              <button @click="$router.push('/avisos/' + a.id)">
                ✏️ Editar
              </button>
  
              <button @click="deletarAviso(a.id)">
                🗑 Deletar
              </button>
            </td>
          </tr>
        </tbody>
  
      </table>
    </div>
  </template>

<script setup>
import { ref, onMounted } from 'vue'
import AvisoService from '../services/AvisoService'
import '../main.css'


const avisos = ref([])
const loading = ref(true)

async function carregarAvisos() {
  try {
    const resp = await AvisoService.getAll()
    avisos.value = resp.data
  } catch (err) {
    console.error('Erro ao carregar avisos:', err)
  } finally {
    loading.value = false
  }
}

async function deletarAviso(id) {
  if (confirm("Tem certeza que deseja deletar este aviso?")) {
    await AvisoService.delete(id)
    await carregarAvisos()
  }
}

onMounted(() => {
  carregarAvisos()
})
</script>


<style scoped>

.container {
  width: 90%;
  margin: 20px auto;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

table, th, td {
  border: 1px solid #ddd;
}

th, td {
  padding: 10px;
}
</style>
