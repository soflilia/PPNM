When you want to find the error of a function F(x,y...) but only know the error of the parameter x,y.., you can use the propagation of error method. For only one parameter it looks like:

F(y) has the error:

$$\sigma_{(F(y))}²= (\dfrac{\partial{F}}{\partial{y}})² * \sigma_y²$$

such that for F(y) = ln(y) we get that:

$$\sigma_{(ln(y))}= \dfrac{(\sigma_y)}{y}$$