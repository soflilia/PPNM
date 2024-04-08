using System.Collections.Generic;
using System; 

public static class QRGS{

    public static void Main() {
    //random generation of matrix A and vector b for tests
    matrix Rnd_A = random_matrix();
    Console.Write($"compiling random matrix A...\n");
    Console.Write($"compiling random vector b with similar dimensions...\n");

    //decomp testing
    (matrix Rnd_Q, matrix Rnd_R) = decomp(Rnd_A);
    //test om R er upper tirangulær
    Console.Write("Testing if R is upper triangular... \n");
    if(R_test(Rnd_R)){Console.Write("passed...\n");}
    else{Console.Write("failed..\n");}

    //test om QTQ=1 
    Console.Write("Testing if Q^T*Q=1.. \n");
    if(QT_test(Rnd_Q)){Console.Write("passed...\n");}
    else{Console.Write("failed...\n");}

    // test om QR=A
    Console.Write("Testing if QR=A.. \n");
    if(A_test(Rnd_A, Rnd_R, Rnd_Q)){Console.Write("passed...\n");}
    else{Console.Write("failed...\n");}

    Console.Write("Testing if Ax=b with the x from solve.. \n");
    matrix Rnd_A1 = random_square();
    (matrix Q1, matrix R1) = decomp(Rnd_A1);
    vector b1 = random_vector(Rnd_A1.size1);
    vector x1 = solve(Q1,R1,b1);
    vector b0 = Rnd_A1*x1;
    //b1.print("Vector b1: ");
    //tb.print("Vector random: ");
    if(vector.approx(b1,b0)){Console.Write("passed...\n");}
    else{Console.Write("failed..\n");}

    // invers test
    Console.Write("Testing if inverse function works. \n");
    matrix N1 = random_square();
    (matrix Q_, matrix R_) = decomp(N1);
    if(inverse_test(N1, inverse(Q_,R_))){Console.Write("passed...\n");}
    else{Console.Write("failed...\n");}
    

    } // Main

//tests here
    private static bool R_test(matrix R){
        //finding the shape of matrix
        int n = R.size1;
        int m = R.size2;
        // skal stadig finde ud af hvordan testen skal se ud...
        for (int j = 0; j<m; j++){ //
            for (int i = j+1; i<n; i++){
                if (R[i,j]!=0){
                    return false;
                }
            }
        }
        return true;
    }

    private static bool QT_test(matrix Q){
        //finding the shape of matrix
        int m = Q.size2;
        // skal stadig finde ud af hvordan testen skal se ud...
        matrix QT = Q.T;
        matrix QTQ = Q.T * Q;
        matrix UNITY = matrix.id(m);
        //UNITY.print();
        //QTQ.print();
        if (!QTQ.approx(UNITY)){
            return false;
        }
        return true;
    }

    private static bool A_test(matrix A,matrix R, matrix Q){
        // skal stadig finde ud af hvordan testen skal se ud...
        matrix QR = Q*R;
        if (!QR.approx(A)){
            return false;
        }
        return true;
    }
    private static bool inverse_test(matrix A, matrix inverse){
        matrix ident = A*inverse;
        if(!ident.approx(matrix.id(A.size1))){
            ident.print();
            return false;
        }
        return true;
    }
// classes here
    public static matrix random_matrix(){
        Random random = new Random();
        // creating m and n with n > m:
        int cols = random.Next(1,50);
        int rows = cols + random.Next(1,50);
        List<vector> data = new List<vector>();
        for(int j=0 ; j<cols ; j++){
            vector col_vecs = new vector(rows);
            for (int i=0; i<rows; i++){
                col_vecs[i] = random.NextDouble()*20-10;
            }
        data.Add(col_vecs);
        }

        matrix rnd1 = new matrix(rows,cols);

        // need the list of vectors to be same format = double[][]
        double [][] data1 = new double[cols][];
        for (int j=0; j<cols; j++){
            data1[j] = data[j];
        }
        rnd1.data = data1 ;
        return rnd1;
        }
    public static matrix random_square(){
        Random random = new Random();
        // creating m and n with n = m:
        int cols = random.Next(2,50);
        int rows = cols;
        List<vector> data = new List<vector>();
        for(int j=0 ; j<cols ; j++){
            vector col_vecs = new vector(rows);
            for (int i=0; i<rows; i++){
                col_vecs[i] = random.NextDouble()*20-10;
            }
        data.Add(col_vecs);
        }

        matrix rnd1 = new matrix(rows,cols);

        // need the list of vectors to be same format = double[][]
        double [][] data1 = new double[cols][];
        for (int j=0; j<cols; j++){
            data1[j] = data[j];
        }
        rnd1.data = data1 ;
        return rnd1;
        }

    public static matrix random_symmetric(){
        Random random = new Random();
        // creating m and n with n = m:
        int cols = random.Next(5,7);
        int rows = cols;
        matrix X = new matrix(rows,cols);
        for(int i =0 ; i<cols; i++){
            for(int j=i; j< rows; j++){
                X[i,j] = X[j,i] = random.NextDouble()*20-10;
                }
            }
            return X;
        }

    public static matrix random_triangular(){
        Random random = new Random();
        // creating m and n with n > m:
        int cols = random.Next(1,4);
        int rows = cols;
        List<vector> data = new List<vector>();
        for(int j=0 ; j<cols ; j++){
            vector col_vecs = new vector(rows);
            for (int i=0; i<rows; i++){
                if (i<=j){col_vecs[i] = random.NextDouble()*20-10;}
                else{col_vecs[i]=0;}
            }
        data.Add(col_vecs);
        }

        matrix rnd1 = new matrix(rows,cols);

        // need the list of vectors to be same format = double[][]
        double [][] data1 = new double[cols][];
        for (int j=0; j<cols; j++){
            data1[j] = data[j];
        }
        rnd1.data = data1 ;
        return rnd1;
        }

    public static vector random_vector(int n){
        Random random = new Random();
        vector x1 = new vector(n);
        for (int i=0 ; i<n; i++){
            x1[i]= random.NextDouble()*20-10;
        }
        return x1;
    }
    public static (matrix,matrix) decomp(matrix A){
        int m = A.size2 ;
        int n = A.size1 ;
        // columnList = [a1,a2,a3] osv som er columnvectors for A
        List<vector> columnList = new List<vector>();
        for (int j=0; j<m ; j++){
            vector col_vec = new vector(n);
            for (int i=0 ; i<n ; i++){
                col_vec[i]= A[i,j] ;
            }
            columnList.Add(col_vec);
        }
        matrix Q=A.copy();
        matrix R = new matrix(m,m); 
        for(int i=0; i<m ; i++){
            // så bliver R defineret udfra A
            R[i,i]= columnList[i].norm();
            // vektor q_i som er Q = [q1,q2....] defineres og Q laves om: Q[i] = q_i
            vector q_i = columnList[i] / R[i,i];
            Q[i] = q_i ; 
            for (int j=i+1; j<m ; j++){
                // da R[i,j] = q_i(transposed)*a_j = dotproduct of the two vectors
                R[i,j] = q_i.dot(columnList[j]) ;
                // a_j laves om så næste iteration giver mening for formlen
                columnList[j] -= q_i*R[i,j]; 
            }
        }
        return (Q,R);
    }

    public static matrix inverse(matrix Q, matrix R){
        int m = R.size1;
        matrix QT = Q.T;
        matrix A_inv = new matrix(m,m);
        for(int cols = 0 ; cols <m ; cols++){
            vector column_A = new vector(m);
            for(int i = m-1; i>=0; i--){
                double sub = 0;
                for(int k = i+1; k<=m-1; k++){
                    sub += R[i,k]*column_A[k];}
                column_A[i]= (QT[i,cols]-sub) / R[i,i];
                A_inv[i,cols] = column_A[i];
            }
        }
        return A_inv;
    }
    
    public static vector solve(matrix Q, matrix R, vector b){
        int m = R.size1;
        //bliver c rigtig??
        vector c = Q.T*b;
        vector x1 = new vector(m);
        //back substitution
        //x1[m-1] = c[m-1]*(1/(R[m-1,m-1]));
        for (int i=m-1; i>=0; i--){
            double sub = 0;
            for (int k = i+1; k<=m-1; k++){
                sub += R[i,k]*x1[k];}
            //Console.Write($"sub = {sub}\n");
            x1[i] = (c[i]-sub) / R[i,i];
        }
        return x1;
    } // solve closed 
} //class QRGS