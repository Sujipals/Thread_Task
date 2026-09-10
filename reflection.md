The deadlock happened because the two threads locked the shared objects in opposite orders.
Thread 1 locked A and waited for B, while Thread 2 locked B and waited for A, so neither thread could continue. 
I fixed the problem by making all threads acquire the locks in the same consistent order: A first and B second.
This is a general rule because using a consistent lock order prevents circular waiting in many multithreaded programs, 
such as a bank system transferring money between two accounts.
