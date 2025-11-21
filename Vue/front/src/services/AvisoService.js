import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:7022/avisos' // ajuste conforme a porta do seu backend
})

export default {
  getAll() {
    return api.get('')
  },

  getById(id) {
    return api.get(`/${id}`)
  },

  create(data) {
    return api.post('/', data)
  },

  update(id, data) {
    return api.put(`/${id}`, data)
  },

  delete(id) {
    return api.delete(`/${id}`)
  }
}
