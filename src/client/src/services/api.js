import axios from 'axios'

const apiClient = axios.create({
  baseURL: 'https://localhost:10500', // Updated from launchSettings.json
  headers: {
    'Content-Type': 'application/json',
  },
})

export default {
  async getQuestions() {
    try {
      const response = await apiClient.get('/api/questions')
      return response.data
    } catch (error) {
      console.error('Failed to fetch questions from PlayGround API:', error)
      throw error
    }
  },
}
