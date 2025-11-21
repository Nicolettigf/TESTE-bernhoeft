<template>
  <div>
    <button @click="$router.push('/')">⬅ Voltar</button>

    <h2>{{ editando ? "Editar Aviso" : "Criar Novo Aviso" }}</h2>

    <div style="display: flex; flex-direction: column; gap: 10px; max-width: 400px;">
      
      <!-- TÍTULO: só editável no CREATE -->
      <input 
        :value="form.titulo"
        @input="!editando ? form.titulo = $event.target.value : null"
        placeholder="Título"
        :disabled="editando"
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
  const id = Number(route.params.id)

  if (id && id <= 0) {
    alert("ID inválido!")
    return router.push("/")
  }

  if (id) {
    editando.value = true

    try {
      const resp = await AvisoService.getById(id)

      form.value.id = resp.data.id
      form.value.titulo = resp.data.titulo    // visível, porém travado
      form.value.mensagem = resp.data.mensagem

    } catch {
      alert("Aviso não encontrado!")
      return router.push("/")
    }
  }
})

async function salvarAviso() {
  // REGRAS DO DESAFIO

  if (!form.value.mensagem || form.value.mensagem.trim() === "")
    return alert("A mensagem é obrigatória!")

  if (!editando.value) {
    if (!form.value.titulo || form.value.titulo.trim() === "")
      return alert("O título é obrigatório!")

    await AvisoService.create({
      titulo: form.value.titulo,
      mensagem: form.value.mensagem
    })

    alert("Aviso criado!")
  } 
  else {
    // UPDATE = só mensagem
    await AvisoService.update(form.value.id, {
      id: form.value.id,
      mensagem: form.value.mensagem
    })

    alert("Aviso atualizado!")
  }

  router.push("/")
}
</script>
