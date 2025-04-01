<template>
  <div class="counter" style='background:#42b883'>{{ count }}</div>
</template>

<script>
import { ref, onMounted, onUnmounted } from 'vue';
import './Counter.css';

export default {
  name: 'Counter',
  setup() {
    const count = ref(0);
    let timer = null;

    onMounted(() => {
      timer = setInterval(() => {
        count.value++;
      }, 1000);
    });

    onUnmounted(() => {
      clearInterval(timer);
    });

    return { count };
  }
}
</script>
