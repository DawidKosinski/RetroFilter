; Retro.asm - Wersja 1.0 - Autor: Dawid Kosiñski
; Ta biblioteka w asemblerze implementuje algorytm nak³adania efektu retro na obraz.

.CODE
retro proc
    ; Inicjalizacja i przygotowanie do pêtli.
    mov ebx, dword ptr[rbp + 48] 
    mov r11, rbx                 
    mov ebx, dword ptr[rbp + 56] 
    mov r10, rbx                 

    ; Wczytanie wspó³czynników do rejestrów.
    movdqu xmm1, oword ptr[rdx]  
    movdqu xmm2, oword ptr[r8]   
    movdqu xmm3, oword ptr[r9]   

    ; Obliczenie d³ugoœci przetwarzanego zakresu.
    sub r10, r11                 
    mov rdi, r10                 
    shr edi, 2                   

    ; Ustawienie wartoœci 4 w rejestrze rax.
    mov rax, 4h                  
    mul r11                      
    add rcx, rax                 

    ; Inicjalizacja licznika pikseli.
    xor r12, r12                 

retroLoop:
    ; Sprawdzenie, czy osi¹gniêto koniec pêtli.
    cmp edi, 0                   
    je endLoop                   

    ; Inkrementacja i sprawdzenie warunków dla piksela.
    inc r12                      
    mov rax, r12
    mov rbx, 11
    cdq
    idiv rbx                     
    cmp rdx, 0               
    je applyWhite            
    cmp rdx, 1                   
    je applyWhite                

    ; Stosowanie efektu retro.
    movdqu xmm0, oword ptr[rcx] 
    mulps xmm0, xmm2           
    movshdup xmm4, xmm0         
    addps xmm0, xmm4            
    movhlps xmm4, xmm0           
    addps xmm0, xmm4            
    punpckldq xmm0, xmm0        
    punpcklqdq xmm0, xmm0       
    addps xmm0, xmm1            
    minps xmm0, xmm3            
    jmp writePixel             

applyWhite:
    ; Nak³adanie bia³ego koloru.
    movdqu xmm0, xmm3          

writePixel:
    ; Zapis przetworzonego piksela.
    movdqu oword ptr[rcx], xmm0 
    add rcx, 16                
    sub rdi, 1                
    jmp retroLoop             

endLoop:
    ret                         
retro endp
END