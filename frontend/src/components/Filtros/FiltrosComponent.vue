<template>
  <aside class="filters-sidebar">
    <div class="filter-section">
      <h3>Filtros</h3>

      <div class="filter-group">
        <label>Buscar:</label>
        <input 
          type="text" 
          v-model="localFilters.search" 
          @input="emitFilters"
          placeholder="Nome do produto..."
          class="search-input"
        />
      </div>

      <div class="filter-group">
        <label>Marcas:</label>
        <div class="checkbox-group">
          <div 
            v-for="marca in marcas" 
            :key="marca.id"
            class="checkbox-item"
          >
            <input 
              type="checkbox" 
              :id="`marca-${marca.id}`"
              :value="marca.id"
              v-model="localFilters.marcaIds"
              @change="emitFilters"
            />
            <label :for="`marca-${marca.id}`">{{ marca.nome }}</label>
          </div>
        </div>
      </div>

      <div class="filter-group">
        <label>Categorias:</label>
        <div class="checkbox-group">
          <div 
            v-for="categoria in categorias" 
            :key="categoria.id"
            class="checkbox-item"
          >
            <input 
              type="checkbox" 
              :id="`categoria-${categoria.id}`"
              :value="categoria.id"
              v-model="localFilters.categoriaIds"
              @change="emitFilters"
            />
            <label :for="`categoria-${categoria.id}`">{{ categoria.nome }}</label>
          </div>
        </div>
      </div>

      <button @click="clearFilters" class="clear-filters-btn">
        Limpar Filtros
      </button>
    </div>
  </aside>
</template>

<script setup>
import { reactive, watch } from 'vue'
import './FiltrosComponent.css'

const props = defineProps({
  modelValue: {
    type: Object,
    required: true
  },
  marcas: {
    type: Array,
    required: true
  },
  categorias: {
    type: Array,
    required: true
  }
})

const emit = defineEmits(['update:modelValue'])

const localFilters = reactive({ ...props.modelValue })

watch(() => props.modelValue, (newVal) => {
  Object.assign(localFilters, newVal)
})

function emitFilters() {
  emit('update:modelValue', { ...localFilters })
  emit('filter-change')
}

function clearFilters() {
  localFilters.search = ''
  localFilters.marcaIds = []
  localFilters.categoriaIds = []
  emitFilters()
}
</script>
