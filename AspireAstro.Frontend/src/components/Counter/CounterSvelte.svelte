<script>
  import { onMount, onDestroy } from 'svelte';
  import './Counter.css';

  let count = 0;
  let timer;
  
  onMount(() => {
    timer = setInterval(() => {
      count++;
    }, 1000);
  });
  
  onDestroy(() => {
    clearInterval(timer);
  });
</script>

<div class="counter" style='background:#f73c00'>{count}</div>
