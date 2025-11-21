<template>
  <div>
    <button @click="$router.push('/')">⬅ Voltar</button>

    <h2>{{ editando ? "Editar Aviso" : "Criar Novo Aviso" }}</h2>

    <div style="display: flex; flex-direction: column; gap: 10px; max-width: 400px;">
      
      <input 
        v-model="form.titulo" 
        placeholder="Título" 
      />

      <textarea 
        v-model="form.mensagem" 
        placeholder="Mensagem"
        rows="5"
      ></textarea>

      <button @click="salvarAviso">
        {{ editando ? "Atualizar" : "Salvar" }}
      </button>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import AvisoService from '../services/AvisoService'

const route = useRoute()
const router = useRouter()

const editando = ref(false)

const form = ref({
  id: null,
  titulo: "",
  mensagem: ""
})

onMounted(async () => {
  const id = route.params.id

  // Se tiver ID → edição
  if (id) {
    editando.value = true

    const resp = await AvisoService.getById(id)

    form.value = {
      id: resp.data.id,
      titulo: resp.data.titulo,
      mensagem: resp.data.mensagem
    }
  }
})

async function salvarAviso() {
  if (!form.value.titulo) return alert("Preencha o título")

  if (editando.value) {
    // UPDATE
    await AvisoService.update(form.value.id, form.value)
    alert("Aviso atualizado!")
  } else {
    // CREATE
    await AvisoService.create(form.value)
    alert("Aviso criado!")
  }

  router.push("/") // volta ao dashboard
}
</script>
