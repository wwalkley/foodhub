<template>
  <v-container class="fill-height">
    <v-responsive class="align-center fill-height">
      <v-table>
        <thead>
        <tr>
          <th class="text-left">
            Name
          </th>
        </tr>
        </thead>
        <tbody>
        <tr
          v-for="item in desserts"
          :key="item.name"
        >
          <td>{{ item.name }}</td>
        </tr>
        </tbody>
      </v-table>
    </v-responsive>
  </v-container>
</template>

<script lang="ts">
import {defineComponent} from 'vue'

export default defineComponent({
  name: "IngredientList",

  data() {
    return {
      desserts: [
        {
          name: 'Frozen Yogurt'
        },
        {
          name: 'Ice cream sandwich'
        }
      ],
    }
  },
})
</script>
<style scoped>

</style>
